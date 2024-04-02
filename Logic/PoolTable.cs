namespace Logic
{
    public class PoolTable
    {
        private int _width;
        private int _height;
        private bool _isActive = false;
        private List<Ball> _balls = new List<Ball>();

        public PoolTable(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void CreateBalls(int ballsQuantity, int radius)
        {
            Random random = new Random();
            for (int i = 0; i < ballsQuantity; i++)
            {
                int x = random.Next(radius, _width - radius);
                int y = random.Next(radius, _height - radius);
                Ball ball = new Ball(x, y, radius);
                _balls.Add(ball);
            }
           
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public List<Ball> Balls
        {
            get { return _balls; }
        }
    }
}
