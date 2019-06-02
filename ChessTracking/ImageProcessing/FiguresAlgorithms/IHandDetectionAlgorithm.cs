using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    interface IHandDetectionAlgorithm
    {
        bool HandDetected(CameraSpacePoint[] cameraSpacePointsFromDepthData, double fieldSize);
    }
}
