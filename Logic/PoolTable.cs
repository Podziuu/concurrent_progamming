namespace Logic
{
    public class PoolTable : LogicAbstractAPI
    {
        private int _width;
        private int _height;
        private bool _isActive = false;
        private List<Ball> _balls = new List<Ball>();
        private Timer? _timer;

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

        //public override void StartGame()
        //{
        //    _timer = new Timer(MoveBalls, null, 0, 8);
        //}

        //public void MoveBalls(Object? stateInfo)
        //{
        //    foreach (Ball ball in _balls) {
        //        if(ball.X + ball.Radius >= _width || ball.X - ball.Radius <= 0)
        //        {
        //            ball.InvertXSpeed();
        //        }

        //        if (ball.Y + ball.Radius >= _height || ball.Y - ball.Radius <= 0)
        //        {
        //            ball.InvertYSpeed();
        //        }

        //        ball.Move();
        //    }
        //}

        //    public override async void StartGame()
        //    {
        //        Random rand = new Random();
        //        while (true)
        //        {
        //            List<Task> moveTasks = new List<Task>();

        //            foreach (Ball ball in _balls)
        //            {
        //                float targetX = rand.Next(ball.Radius, _width - ball.Radius);
        //                float targetY = rand.Next(ball.Radius, _height - ball.Radius);
        //                //moveTasks.Add(Task.Run(async () => await ball.Move(targetX, targetY, 10)));
        //                Task moveTask = ball.Move(targetX, targetY, 10);
        //            }
        //            //await Task.WhenAll(moveTasks);
        //            await Task.WhenAll(_balls.Select(ball => ball.MoveTask));
        //        }
        //}

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
