using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Game;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;
using ChessTracking.Utils;

namespace ChessTracking.ControllingElements
{
    class TrackingResultProcessing
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private GameController GameController { get; }
        private FPSCounter FpsCounter { get; }

        /// <summary>
        /// Indicated whether figures are located
        /// </summary>
        private bool TrackningInProgress { get; set; }

        /// <summary>
        /// Defines number of right rotation between tracking state and game representation
        /// </summary>
        private int NumberOfCwRotations { get; set; }

        /// <summary>
        /// Queue containing latest tracking states, so they can be averaged
        /// </summary>
        private Queue<TimestampObject<TrackingState>> AveragingQueue { get; set; }

        /// <summary>
        /// Last state sent to game component
        /// </summary>
        private TrackingState LastSentState { get; set; }

        /// <summary>
        /// Offset of state processing after reset
        /// </summary>
        private DateTime TimeOffset { get; set; }

        /// <summary>
        /// Bitmap of chessboard for displaying chessboard tracking state
        /// </summary>
        private static Bitmap ChessboardBitmap { get; set; }

        public TrackingResultProcessing(UserInterfaceOutputFacade outputFacade, GameController gameController)
        {
            OutputFacade = outputFacade;
            GameController = gameController;
            FpsCounter = new FPSCounter();
            TrackningInProgress = false;
            AveragingQueue = new Queue<TimestampObject<TrackingState>>();
            TimeOffset = DateTime.Now + TimeSpan.FromSeconds(1.5);
        }

        public void Reset()
        {
            TrackningInProgress = false;
            LastSentState = null;
            NumberOfCwRotations = 0;
            AveragingQueue.Clear();
            TimeOffset = DateTime.Now + TimeSpan.FromSeconds(1.5);
        }

        /// <summary>
        /// Process result message, update concrete components, average tracking state optionally notify game
        /// </summary>
        /// <param name="resultMessage"></param>
        public void ProcessResult(ResultMessage resultMessage)
        {
            OutputFacade.HandDetected(resultMessage.HandDetected);
            OutputFacade.DisplayVizuaization(resultMessage.BitmapToDisplay);
            UpdateFps();

            if (resultMessage.HandDetected)
                return;
            if (TimeOffset > DateTime.Now)
                return;

            var trackingState = resultMessage.TrackingState;
            trackingState.HorizontalFlip();

            if (TrackningInProgress)
            {
                trackingState.RotateClockWise(NumberOfCwRotations);
                OutputFacade.UpdateImmediateBoard(GenerateImageForTrackingState(trackingState));
                // averaging
                var average = Averaging(trackingState);
                if (average == null)
                    return;
                // send averaging
                OutputFacade.UpdateAveragedBoard(GenerateImageForTrackingState(average));
                // check so we aren't sending the same state again
                if (LastSentState != null && !LastSentState.IsEquivalentTo(average))
                {
                    // send it to the game
                    GameController.TryChangeChessboardState(average);
                }

                LastSentState = average;
            }
            else
            {
                OutputFacade.UpdateImmediateBoard(GenerateImageForTrackingState(trackingState));
                // averaging
                var average = Averaging(trackingState);
                if (average == null)
                    return;
                else
                {
                    // send averaging
                    OutputFacade.UpdateAveragedBoard(GenerateImageForTrackingState(average));
                    // try to get rotation of tracking state
                    var rotation = GameController.InitiateWithTracingInput(average);
                    // if rotation is succesfull(figures got matched)
                    if (rotation.HasValue)
                    {
                        NumberOfCwRotations = rotation.Value;
                        RotatedSavedStates();
                        TrackningInProgress = true;
                    }
                }
            }
        }

        /// <summary>
        /// Rotate currently saved states in averaging queue
        /// </summary>
        private void RotatedSavedStates()
        {
            foreach (var averageState in AveragingQueue)
            {
                averageState.StoredObject.RotateClockWise(NumberOfCwRotations);
            }
        }

        /// <summary>
        /// Performs averaging of currently saved states
        /// </summary>
        /// <param name="trackingState">Arrived state</param>
        /// <returns>Averaged result</returns>
        private TrackingState Averaging(TrackingState trackingState)
        {
            AveragingQueue.Enqueue(new TimestampObject<TrackingState>(trackingState));

            var now = DateTime.Now;

            // discard all states older than x seconds
            var temp = AveragingQueue.ToList();
            temp.RemoveAll(x => Math.Abs((now - x.Timestamp).Seconds) > 2);

            AveragingQueue = new Queue<TimestampObject<TrackingState>>(temp);
            
            // don't average if there aren't enough samples
            if (AveragingQueue.Count < 5)
                return null;
            
            // choose most common tracking state
            List<Tuple<TrackingState, int>> aggregation = new List<Tuple<TrackingState, int>>();
            foreach (var state in AveragingQueue)
            {
                if (aggregation.Any(x => x.Item1 == state.StoredObject))
                {
                    var old = aggregation.Single(x => x.Item1 == state.StoredObject);
                    aggregation.Remove(old);
                    aggregation.Add(new Tuple<TrackingState, int>(old.Item1, old.Item2 + 1));
                }
                else
                {
                    aggregation.Add(new Tuple<TrackingState, int>(state.StoredObject, 1));
                }
            }

            return aggregation.OrderByDescending(x => x.Item2).First().Item1;
        }

        /// <summary>
        /// Update fps counter
        /// </summary>
        private void UpdateFps()
        {
            int? fps = FpsCounter.Update();
            if (fps != null)
            {
                OutputFacade.UpdateFps(fps.Value);
            }
        }
        
        static TrackingResultProcessing()
        {
            ChessboardBitmap = Properties.Resources.ChessboardSmaller;
        }

        /// <summary>
        /// Render tracking state image for displaying
        /// </summary>
        private Bitmap GenerateImageForTrackingState(TrackingState trackingState)
        {
            trackingState = new TrackingState(trackingState.Figures);

            var bm = (Bitmap)ChessboardBitmap.Clone();
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    using (Graphics graphics = Graphics.FromImage(bm))
                    {
                        // invertion of y coordinate due to differences between chess and bitmap coordinates
                        switch (trackingState.Figures[x, 7 - y])
                        {
                            case TrackingFieldState.White:
                                graphics.FillRectangle(whiteBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                break;
                            case TrackingFieldState.Black:
                                graphics.FillRectangle(blackBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                break;
                            case TrackingFieldState.None:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            return bm;
        }
    }
}

