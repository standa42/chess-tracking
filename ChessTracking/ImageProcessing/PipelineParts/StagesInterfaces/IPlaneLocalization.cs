using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    /// <summary>
    /// Wraps up whole functionality that detects plane with chessboard
    /// </summary>
    interface IPlaneLocalization
    {
        /// <summary>
        /// Method called while calibrating to the given scene
        /// </summary>
        /// <param name="inputData">Data from kinect sensor</param>
        /// <returns>Data with calibrated plane</returns>
        PlaneDoneData Calibrate(InputData inputData);

        /// <summary>
        /// Method called while tracking given scene
        /// </summary>
        /// <param name="inputData">Data from kinect sensor</param>
        /// <returns>Data with tracked plane</returns>
        PlaneDoneData Track(InputData inputData);
    }
}
