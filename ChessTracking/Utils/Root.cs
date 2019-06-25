namespace ChessTracking.Utils
{
    
        class Root
        {
            public int SelfReferenceNumber { get; }
            public int X { get; }
            public int Y { get; }

            public int Count;

            public Root(int nr, int _x, int _y)
            {
                SelfReferenceNumber = nr;
                X = _x;
                Y = _y;
                Count = 0;
            }
        }
}
