namespace Logic
{
    public class PoolTable : LogicAbstractAPI
    {
        private int _width;
        private int _height;
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
                float x = random.Next(radius, _width - radius);
                float y = random.Next(radius, _height - radius);
                Ball ball = new Ball(x, y, radius);
                _balls.Add(ball);
            }
        }

        public override void StartGame()
        {
            Random rand = new Random();
            List<Task> moveTasks = new List<Task>();

            foreach (Ball ball in _balls)
            {
                Thread thread = new Thread(() => { ball.Move(_width, _height); });
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
