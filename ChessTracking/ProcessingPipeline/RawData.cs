using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline
{
    class RawData
    {
        public byte[] ColorFrameData { get; set; }
        public ushort[] DepthData { get; set; }
        public ushort[] InfraredData { get; set; }
        public CameraSpacePoint[] CameraSpacePointsFromDepthData { get; set; }
        public DepthSpacePoint[] PointsFromColorToDepth { get; set; }
        public ColorSpacePoint[] PointsFromDepthToColor { get; set; }

        public VisualisationType VisualisationType { get; set; }

        public RawData(KinectResourcesMessage resources, VisualisationType visualisationType)
        {
            this.ColorFrameData = resources.ColorFrameData;
            this.DepthData = resources.DepthData;
            this.InfraredData = resources.InfraredData;
            this.CameraSpacePointsFromDepthData = resources.CameraSpacePointsFromDepthData;
            this.PointsFromColorToDepth = resources.PointsFromColorToDepth;
            this.PointsFromDepthToColor = resources.PointsFromDepthToColor;

            VisualisationType = visualisationType;
        }
    }
}
