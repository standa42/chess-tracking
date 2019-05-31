using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class UserDefinedParameters
    {
        public VisualisationType VisualisationType { get; set; } = VisualisationType.RawRGB;
        public double ColorCalibrationAdditiveConstant { get; set; } = 0;

        public UserDefinedParameters GetShallowCopy()
        {
            return (UserDefinedParameters)MemberwiseClone();
        }
    }
}
