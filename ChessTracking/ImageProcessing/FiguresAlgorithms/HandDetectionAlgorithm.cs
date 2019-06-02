using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    class HandDetectionAlgorithm : IHandDetectionAlgorithm
    {
        public bool HandDetected(CameraSpacePoint[] csp, double fieldSize)
        {
            int counter = 0;

            for (int i = 0; i < csp.Length; i++)
            {
                var chessboardSize = fieldSize * 8;
                var additionalSpaceOutsideOfChessboard = fieldSize * (7 / 10f);

                var pointIsValid = !(float.IsInfinity(csp[i].Z) || float.IsNaN(csp[i].Z));
                var pointIsHighEnoughtAccordingToThreshold = csp[i].Z < -0.095f;
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
