using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    /// <summary>
    /// Wraps up whole functionality that looks for position of chessboard
    /// </summary>
    interface IChessboardLocalization
    {
        /// <summary>
        /// Method called while calibrating to the given scene
        /// </summary>
        /// <param name="planeData">Data from previous stages of calibration, especially plane calibration</param>
        /// <returns>Data with calibrated chessboard position</returns>
        ChessboardTrackingCompleteData Calibrate(PlaneTrackingCompleteData planeData);

        /// <summary>
        /// Method called while tracking given scene
        /// </summary>
        /// <param name="planeData">Data from previous stages of tracking, especially plane tracking</param>
        /// <returns>Data with tracked chessboard</returns>
        ChessboardTrackingCompleteData Track(PlaneTrackingCompleteData planeData);
    }
}
