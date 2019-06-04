using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    interface IRotateSpaceToChessboard
    {
        void Rotate(Chessboard3DReprezentation boardRepprezentation, CameraSpacePoint[] cspFromdd);
    }
}
