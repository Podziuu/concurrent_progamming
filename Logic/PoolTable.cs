namespace Logic
{
    public class PoolTable : LogicAbstractAPI
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

        public override void CreateBalls(int ballsQuantity, int radius)
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

        public override async Task StartGame()
        {
            Random rand = new Random();
            foreach (Ball ball in _balls)
            {
                float targetX = rand.Next(ball.Radius, _width - ball.Radius);
                float targetY = rand.Next(ball.Radius, _height - ball.Radius);
                //await ball.Move(targetX, targetY, 10);
                Thread thread = new Thread(() => ball.Move(targetX, targetY, 10));
                thread.Start();
            }
        }

        public override void StopGame()
        {
            _balls.Clear();
        }

        public override int Width
        {
            get { return _width; }
        }

        public override int Height
        {
            get { return _height; }
        }

        //public bool IsActive
        //{
        //    get { return _isActive; }
        //    set { _isActive = value; }
        //}

        public override List<Ball> Balls
        {
            get { return _balls; }
        }

        public override List<Ball> GetAllBalls()
        {
            return _balls;
        }
    }
}
