using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using ChessTracking.ControllingElements.ProgramState;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.General;
using ChessTracking.MultithreadingMessages;
using ChessTracking.MultithreadingMessages.FromProcessing;
using ChessTracking.MultithreadingMessages.ToProcessing;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements
{
    /// <summary>
    /// Maintains communication with tracking thread
    /// </summary>
    class TrackingManager
    {
        public UserInterfaceOutputFacade OutputFacade { get; }
        private TrackingResultProcessing TrackingResultProcessing { get; }
        private Kinect Kinect { get; set; }
        private IProgramState ProgramState { get; }

        /// <summary>
        /// Queue of messages arriving from tracking thread
        /// </summary>
        public BlockingCollection<Message> ProcessingOutputQueue { get; }

        /// <summary>
        /// Queue pointing to tracking thread
        /// </summary>
        private BlockingCollection<Message> ProcessingCommandsQueue { get; }

        public TrackingManager(UserInterfaceOutputFacade outputFacade, TrackingResultProcessing trackingResultProcessing, UserDefinedParametersPrototypeFactory userParameters, IProgramState programState)
        {
            OutputFacade = outputFacade;
            TrackingResultProcessing = trackingResultProcessing;
            ProgramState = programState;

            ProcessingCommandsQueue = new BlockingCollection<Message>(new ConcurrentQueue<Message>());
            ProcessingOutputQueue = new BlockingCollection<Message>(new ConcurrentQueue<Message>());

            InitPipelineThread(userParameters);
        }

        /// <summary>
        /// Initialization of tracking thread
        /// </summary>
        private void InitPipelineThread(UserDefinedParametersPrototypeFactory userParameters)
        {
            var thread = new Thread(() =>
            {
                var processingController =
                    new PipelineController(ProcessingCommandsQueue, ProcessingOutputQueue, userParameters);
                processingController.Start();
            }) {IsBackground = true};
            thread.Start();
        }

        public void StartTracking()
        {
            var buffer = new KinectDataBuffer();

            ProgramState.StartedTracking();
            ProcessingCommandsQueue.Add(new StartTrackingMessage(buffer));
            Kinect = new Kinect(ProcessingCommandsQueue, buffer);
        }

        public void StopTracking(bool gameFinished = false)
        {
            TrackingResultProcessing.Reset();
            Kinect.Dispose();
            Kinect = null;
            if(!gameFinished)
                ProgramState.StoppedTracking();
        }

        public void Recalibrate()
        {
            TrackingResultProcessing.Reset();
            ProgramState.Recalibrating();
            ProcessingCommandsQueue.Add(new RecalibrateMessage());
        }

        public void SendChessboardMovement(ChessboardMovement movement)
        {
            ProcessingCommandsQueue.Add(new ChessboardMovementMessage(movement));
        }
        
        /// <summary>
        /// Processing of messages arriving from tracking thread
        /// </summary>
        public void ProcessQueue()
        {
            bool messageProcessed = false;

            do
            {
                messageProcessed = ProcessingOutputQueue.TryTake(out var message);

                if (message is ResultMessage resultMessage)
                    TrackingResultProcessing.ProcessResult(resultMessage);

                if (message is TrackingStartSuccessfulMessage)
                    ProgramState.TrackingStartSuccessful();

                if (message is TrackingError error)
                {
                    ProgramState.ErrorInTracking(error.Message);
                    StopTracking();
                }

                if (message is SceneCalibrationSnapshotMessage snapshot)
                {
                    OutputFacade.UpdateCalibrationSnapshot(snapshot.Snapshot);
                }
                    

                if (messageProcessed && message == null)
                {
                    throw new InvalidOperationException($"Unexpected incoming message in {nameof(TrackingManager)}");
                }

            } while (messageProcessed);
        }
    }
}
