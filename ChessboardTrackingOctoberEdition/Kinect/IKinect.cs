using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Kinect
{
    public delegate void KinectMultiframeArrived(
        byte[] colorFrameData,
        ushort[] depthData,
        ushort[] infraredData,
        CameraSpacePoint[] cameraSpacePointsFromDepthData,
        DepthSpacePoint[] pointsFromColorToDepth,
        ColorSpacePoint[] pointsFromDepthToColor
    );

    interface IKinect : IDisposable
    {
        event KinectMultiframeArrived KinectMultiframeArrived;
        CancellationToken Token { get; set; }

        FrameDescription ColorFrameDescription { get; }
        FrameDescription DepthFrameDescription { get; }
        FrameDescription InfraredFrameDescription { get; }
    }
}
