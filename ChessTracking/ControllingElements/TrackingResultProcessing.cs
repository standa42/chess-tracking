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
        private TrackingState LastSentState { get; set; }

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
            trackingState.HorizontalFlip();

            if (TrackningInProgress)
            {
                trackingState.RotateClockWise(NumberOfCWRotations);
                OutputFacade.UpdateImmediateBoard(GenerateImageForTrackingState(trackingState));
                // průměrování
                var average = Averaging(trackingState);
                // poslat průměrování
                OutputFacade.UpdateAveragedBoard(GenerateImageForTrackingState(average));
                // ověřit, zda neposíláme znovu stejný stav
                if (LastSentState != null && !LastSentState.IsEquivalentTo(average))
                {
                    // poslat to do game
                    GameController.TryChangeChessboardState(average);
                }

                LastSentState = average;
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
                        RotatedSavedStates();
                        TrackningInProgress = true;
                    }
                }
            }
        }

        private void RotatedSavedStates()
        {
            foreach (var averageState in AveragingQueue)
            {
                averageState.RotateClockWise(NumberOfCWRotations);
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

        private static Bitmap ChessboardBitmap { get; set; }

        static TrackingResultProcessing()
        {
            ChessboardBitmap = new Bitmap($@"img\ChessboardSmaller4.png");
        }

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

