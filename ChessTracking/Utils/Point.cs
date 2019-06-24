using ChessTracking.ImageProcessing.PlaneAlgorithms;

namespace ChessTracking.Utils
{
    
        struct Point
        {
            public int x;
            public int y;
            public int position;

            public Point(int _x, int _y)
            {
                x = _x;
                y = _y;
                position = x + y * PlaneLocalizationConfig.DepthImageWidth;
            }
        }
        
}
