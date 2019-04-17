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

namespace ChessTracking.ControllingElements
{
    class TrackingResultProcessing
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private GameController GameController { get; }
        private FPSCounter FpsCounter { get; }
        private bool TrackningInProgress { get; set; }
        private int NumberOfCWRotations { get; set; }
        private Queue<TrackingState> AveragingQueue { get; set; }

        public TrackingResultProcessing(UserInterfaceOutputFacade outputFacade, GameController gameController)
        {
            OutputFacade = outputFacade;
            GameController = gameController;
            FpsCounter = new FPSCounter();
            TrackningInProgress = false;
            AveragingQueue = new Queue<TrackingState>();
        }

        public void Reset()
        {
            TrackningInProgress = false;
            AveragingQueue.Clear();
        }

        public void ProcessResult(ResultMessage resultMessage)
        {
            OutputFacade.HandDetected(resultMessage.HandDetected);
            OutputFacade.DisplayVizuaization(resultMessage.BitmapToDisplay);
            UpdateFps();

            if (resultMessage.HandDetected == "true")
                return;

            var trackingState = resultMessage.TrackingState;

            if (TrackningInProgress)
            {
                trackingState.RotateClockWise(NumberOfCWRotations);
                OutputFacade.UpdateImmediateBoard(GenerateImageForTrackingState(trackingState));
                // průměrování
                var average = Averaging(trackingState);
                // poslat průměrování
                OutputFacade.UpdateAveragedBoard(GenerateImageForTrackingState(average));
                // poslat to do game
                GameController.TryChangeChessboardState(average);
            }
            else
            {
                OutputFacade.UpdateImmediateBoard(GenerateImageForTrackingState(trackingState));
                // průměrování
                var average = Averaging(trackingState);
                if (average == null)
                    return;
                else
                {
                    // poslat průměrování
                    OutputFacade.UpdateAveragedBoard(GenerateImageForTrackingState(average));
                    // pokus to poslat do game a získat otočení
                    var rotation = GameController.InitiateWithTracingInput(average);
                    // přeponout podle toho trackinginprogress, popř otočit to co mám v paměti
                    if (rotation.HasValue)
                    {
                        NumberOfCWRotations = rotation.Value;
                        TrackningInProgress = true;
                    }
                }
            }
        }

        private TrackingState Averaging(TrackingState trackingState)
        {
            AveragingQueue.Enqueue(trackingState);

            if (AveragingQueue.Count < 10)
                return null;

            AveragingQueue.Dequeue();

            List<Tuple<TrackingState, int>> aggregation = new List<Tuple<TrackingState, int>>();

            foreach (var state in AveragingQueue)
            {
                if (aggregation.Any(x => x.Item1 == state))
                {
                    var old = aggregation.Single(x => x.Item1 == state);
                    aggregation.Remove(old);
                    aggregation.Add(new Tuple<TrackingState, int>(old.Item1, old.Item2 + 1));
                }
                else
                {
                    aggregation.Add(new Tuple<TrackingState, int>(state, 1));
                }
            }

            return aggregation.OrderByDescending(x => x.Item2).First().Item1;
        }

        private void UpdateFps()
        {
            int? fps = FpsCounter.Update();
            if (fps != null)
            {
                OutputFacade.UpdateFps(fps.Value);
            }
        }

        private Bitmap GenerateImageForTrackingState(TrackingState trackingState)
        {
            trackingState = new TrackingState(trackingState.Figures);
            trackingState.RotateClockWise(2);

            var bm = new Bitmap(320, 320, PixelFormat.Format24bppRgb);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    using (Graphics graphics = Graphics.FromImage(bm))
                    {
                        switch (trackingState.Figures[x, y])
                        {
                            case TrackingFieldState.White:
                                graphics.FillRectangle(whiteBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                break;
                            case TrackingFieldState.Black:
                                graphics.FillRectangle(blackBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                break;
                            case TrackingFieldState.None:
                                graphics.FillRectangle(blueBrush, new Rectangle(x * 40, y * 40, 40, 40));
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

