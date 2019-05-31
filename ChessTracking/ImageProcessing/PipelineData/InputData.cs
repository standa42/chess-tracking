using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class InputData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public UserDefinedParameters UserParameters { get; set; }
        
        public InputData(KinectData kinectData, UserDefinedParameters userParameters)
        {
            this.KinectData = kinectData;
            this.UserParameters = userParameters;
            this.ResultData = new TrackingResultData();
        }
    }
}
