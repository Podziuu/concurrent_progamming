namespace Logic
{
    public class Ball
    {
        private int _x;
        private int _y;
        private int _radius;
        
        public Ball(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public void Move(int dx, int dy)
        {
            _x += dx;
            _y += dy;
        }

    }
}
