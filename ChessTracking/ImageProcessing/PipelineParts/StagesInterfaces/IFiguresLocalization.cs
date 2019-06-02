using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    /// <summary>
    /// Wraps up whole functionality that detects objects on and over chessboard
    /// </summary>
    interface IFiguresLocalization
    {
        /// <summary>
        /// Method called while calibrating to the given scene
        /// </summary>
        /// <param name="chessboardData">Data from previous stages of calibration, especially chessboard calibration</param>
        /// <returns>Data with calibration informations about figures</returns>
        FiguresDoneData Calibrate(ChessboardDoneData chessboardData);

        /// <summary>
        /// Method called while tracking given scene
        /// </summary>
        /// <param name="chessboardData">Data from previous stages of tracking, especially chessboard tracking</param>
        /// <returns>Data with tracked figures</returns>
        FiguresDoneData Track(ChessboardDoneData chessboardData);
    }
}
