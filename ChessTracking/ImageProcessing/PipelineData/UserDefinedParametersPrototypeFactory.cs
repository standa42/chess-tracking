using System;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Prototype factory responsible for user defined parameters. Thread safe.
    /// </summary>
    class UserDefinedParametersPrototypeFactory
    {
        private UserDefinedParameters Prototype { get; set; } = new UserDefinedParameters();
        private object Lock { get; } = new object();
        
        /// <summary>
        /// Get copy of factory prototype
        /// </summary>
        /// <returns></returns>
        public UserDefinedParameters GetShallowCopy()
        {
            UserDefinedParameters copy;
            lock (Lock)
            {
                copy = Prototype.GetShallowCopy();
            }
            return copy;
        }

        /// <summary>
        /// Substitute new prototype to factory
        /// </summary>
        /// <param name="newPrototype"></param>
        public void SubstitutePrototype(UserDefinedParameters newPrototype)
        {
            lock (Lock)
            {
                Prototype = newPrototype;
            }
        }

        /// <summary>
        /// Change prototype with action
        /// </summary>
        public void ChangePrototype(Action<UserDefinedParameters> changeOperation)
        {
            lock (Lock)
            {
                changeOperation(Prototype);
            }
        }
    }
}
