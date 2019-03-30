using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    interface IVisualiser
    {
        void Update(
            byte[] colorFrameData,
            ushort[] depthData,
            ushort[] infraredData,
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth,
            ColorSpacePoint[] pointsFromDepthToColor
            );
    }
}
