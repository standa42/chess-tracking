using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class UserDefinedParametersPrototypeFactory
    {
        private UserDefinedParameters Prototype { get; set; } = new UserDefinedParameters();
        private object Lock { get; } = new object();
        
        public UserDefinedParameters GetShallowCopy()
        {
            UserDefinedParameters copy;
            lock (Lock)
            {
                copy = Prototype.GetShallowCopy();
            }
            return copy;
        }

        public void SubstitutePrototype(UserDefinedParameters newPrototype)
        {
            lock (Lock)
            {
                Prototype = newPrototype;
            }
        }

        public void ChangePrototype(Action<UserDefinedParameters> changeOperation)
        {
            lock (Lock)
            {
                changeOperation(Prototype);
            }
        }
    }
}
