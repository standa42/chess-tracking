using System;
using System.Collections.Concurrent;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Data buffer holding kinect data, accesible from multiple threads
    /// </summary>
    class KinectDataBuffer
    {
        private BlockingCollection<KinectData> Data { get; } = new BlockingCollection<KinectData>();

        public bool IsEmpty()
        {
            return Data.Count == 0;
        }

        /// <summary>
        /// Store data in buffer - blocking call
        /// </summary>
        public void Store(KinectData data)
        {
            if(IsEmpty())
                Data.Add(data);
        }

        /// <summary>
        /// Try to get data from buffer in given interval
        /// </summary>
        /// <returns>Data if successful, null otherwise</returns>
        public KinectData TryTake(int milisecondsTimeout = 200)
        {
            var success = Data.TryTake(out var item, milisecondsTimeout);

            if (success)
                return item;
            else
                return null;
        }

        /// <summary>
        /// Try to get data from buffer - blocking call
        /// </summary>
        public KinectData Take()
        {
            return Data.Take();
        }
        
    }
}
