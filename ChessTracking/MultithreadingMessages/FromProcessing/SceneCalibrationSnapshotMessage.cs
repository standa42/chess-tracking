using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages.FromProcessing
{
    /// <summary>
    /// Message containing images taken during calibration for user to have visual feedback
    /// </summary>
    class SceneCalibrationSnapshotMessage : Message
    {
        public SceneCalibrationSnapshot Snapshot { get; }

        public SceneCalibrationSnapshotMessage(SceneCalibrationSnapshot snapshot)
        {
            Snapshot = snapshot;
        }
    }
}
