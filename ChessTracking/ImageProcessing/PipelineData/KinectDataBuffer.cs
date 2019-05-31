using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class KinectDataBuffer
    {
        private BlockingCollection<KinectData> Data { get; } = new BlockingCollection<KinectData>();

        public bool IsEmpty()
        {
            return Data.Count == 0;
        }

        public void Store(KinectData data)
        {
            Data.Add(data);
        }

        public KinectData TryTake()
        {
            var success = Data.TryTake(out var item, 200);

            if (success)
                return item;
            else
                return null;
        }

        public KinectData Take()
        {
            return Data.Take();
        }

    }
}
