namespace Barbar.Euler
{
    public struct Point2D
    {
        private int m_X;
        private int m_Y;


        public int X
        {
            get { return m_X; }
        }

        public int Y
        {
            get { return m_Y; }
        }

        public override int GetHashCode()
        {
            return m_X ^ m_Y;
        }

        public override bool Equals(object obj)
        {
            var another = (Point2D)obj;
            return m_X == another.X && m_Y == another.Y;
        }

        public Point2D(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }
    }
}
