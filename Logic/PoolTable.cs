using Data;

namespace Logic
{
    internal class PoolTable : LogicAbstractAPI
    {
        private int _width;
        private int _height;
        private List<IBall> _balls = new List<IBall>();
        private DataAbstractAPI _data;

        public PoolTable(int width, int height, DataAbstractAPI data)
        {
            _width = width;
            _height = height;
            _data = data;
        }

        public override void CreateBalls(int ballsQuantity, int radius)
        {
            Random random = new Random();
            for (int i = 0; i < ballsQuantity; i++)
            {
                float x = random.Next(radius, _width - radius);
                float y = random.Next(radius, _height - radius);
                IBall ball = IBall.CreateBall(x, y, radius);
                _balls.Add(ball);
            }
        }

        public override void StartGame()
        {
            foreach (Ball ball in _balls)
            {
                Thread thread = new Thread(() => { ball.Move(_width, _height); });
                thread.Start();
            }
        }

        public override void StopGame()
        {
            foreach (Ball ball in _balls)
            {
                ball.IsMoving = false;
            }

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


        public override List<IBall> GetAllBalls()
        {
            return _balls;
        }
    }
}
