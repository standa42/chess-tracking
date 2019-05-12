using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ProcessingPipeline.Plane
{
    /// <summary>
    /// Random seed triangles for ransac plane matching algorithm
    /// </summary>
    public class RansacSeedTriangle
    {
        public int FirstVertexIndex;
        public int SecondVertexIndex;
        public int ThirdVertexIndex;

        public RansacSeedTriangle(int firstVertexIndex, int secondVertexIndex, int thirdVertexIndex)
        {
            FirstVertexIndex = firstVertexIndex;
            SecondVertexIndex = secondVertexIndex;
            ThirdVertexIndex = thirdVertexIndex;
        }
    }
}
