using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class CalibrationException : Exception
    {
        public CalibrationException() : base() { }
        public CalibrationException(string message) : base(message) { }
        public CalibrationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
