using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class UserDefinedParametersFactory
    {
        private object Lock { get; } = new object();

        private UserDefinedParameters _prototype = new UserDefinedParameters();

        public UserDefinedParameters Prototype
        {
            get
            {
                UserDefinedParameters copy;
                lock (Lock)
                {
                    copy = _prototype.GetShallowCopy();
                }
                return copy;
            }
            set
            {
                lock (Lock)
                {
                    _prototype = value;
                }
            }
        }
    }
}
