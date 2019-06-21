using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages.FromProcessing
{
    class SceneCalibrationSnapshotMessage : Message
    {
        public SceneCalibrationSnapshot Snapshot { get; }

        public SceneCalibrationSnapshotMessage(SceneCalibrationSnapshot snapshot)
        {
            Snapshot = snapshot;
        }
    }
}
