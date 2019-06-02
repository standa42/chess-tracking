using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    class HandDetectionAlgorithm : IHandDetectionAlgorithm
    {
        /// <summary>
        /// Decides if there is hand, or some other object over the chessboard
        /// -> counts points in specific area over the chessboard
        /// -> if they exceed threshold, detection is considered as positive
        /// </summary>
        /// <param name="csp">Camera space points from sensor</param>
        /// <param name="fieldSize">Size of chessboard field</param>
        /// <returns>Presence of object over chessboard</returns>
        public bool HandDetected(CameraSpacePoint[] csp, double fieldSize)
        {
            int counter = 0;

            for (int i = 0; i < csp.Length; i++)
            {
                var chessboardSize = fieldSize * 8;
                var additionalSpaceOutsideOfChessboard = fieldSize * (7 / 10f);

                var pointIsValid = !(float.IsInfinity(csp[i].Z) || float.IsNaN(csp[i].Z));
                var pointIsHighEnoughtAccordingToThreshold = csp[i].Z < -0.11f;
                var pointIsLowEnoughtAccordingToThreshold = csp[i].Z > -0.2f;
                var lowerBoundOfXCoordinateIsOk = csp[i].X > -additionalSpaceOutsideOfChessboard;
                var upperBoundOfXCoordinateIsOk = csp[i].X < chessboardSize + additionalSpaceOutsideOfChessboard;
                var lowerBoundOfYCoordinateIsOk = csp[i].Y > -additionalSpaceOutsideOfChessboard;
                var upperBoundOfYCoordinateIsOk = csp[i].Y < chessboardSize + additionalSpaceOutsideOfChessboard;

                if (pointIsValid 
                    && pointIsHighEnoughtAccordingToThreshold 
                    && pointIsLowEnoughtAccordingToThreshold 
                    && lowerBoundOfXCoordinateIsOk 
                    && lowerBoundOfYCoordinateIsOk 
                    && upperBoundOfYCoordinateIsOk 
                    &&upperBoundOfXCoordinateIsOk)
                {
                    counter++;
                }
            }

            var NumberOfPointsInAreaIndicatingPresenceOfHand = 20;
            return counter > NumberOfPointsInAreaIndicatingPresenceOfHand;
        }
    }
}
