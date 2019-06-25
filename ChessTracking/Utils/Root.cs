namespace ChessTracking.Utils
{
    /// <summary>
    /// Root element intended for multiple breath-first searches in image
    /// </summary>
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
