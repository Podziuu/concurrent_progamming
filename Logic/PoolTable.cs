using Data;

namespace Logic
{
    internal class PoolTable : LogicAbstractAPI, IObserver<IBall>, IObservable<LogicAbstractAPI>
    {
        private DataAbstractAPI _data;
        private readonly object ballLock = new object();
        private IDisposable unsubscriber;
        List<IObserver<LogicAbstractAPI>> _observers;

        public PoolTable(DataAbstractAPI data)
        {
            _data = data;
            _observers = new List<IObserver<LogicAbstractAPI>>();
        }

        public override void StartGame()
        {
            foreach (IBall ball in _data.GetAllBalls())
            {
                //this.Subscribe(ball);
                Thread thread = new Thread(() => { ball.Move(); });
                thread.Start();
            }
        }

        public override void StopGame()
        {
            foreach (IBall ball in _data.GetAllBalls())
            {
                ball.IsMoving = false;
            }

            _data.RemoveBalls();
        }

        public override void CreateBalls(int ballsQuantity, int radius)
        {
            List<IBall> balls =_data.CreateBalls(ballsQuantity, radius);
            foreach (IBall ball in balls)
            {
                this.Subscribe(ball);
            }
            //List<IBall> createdBalls = _data.GetAllBalls();

            //foreach (var observer in _observers)
            //{
            //    observer.OnNext(createdBalls);
            //}
        }

        public override List<IBall> GetAllBalls()
        {
            return _data.GetAllBalls();
        }


        public void Subscribe(IObservable<IBall> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IBall value)
        {
            lock (ballLock)
            {
                foreach (IBall ball in _data.GetAllBalls())
                {
                    WallCollision(ball);
                    BallCollision(ball);
                }

                // forward data to Model layer
                foreach (var observer in _observers)
                {
                    observer.OnNext(this);
                }
            }
            //_balls.Add(value);
        }

        private void WallCollision(IBall ball)
        {
            if (ball.X - ball.Radius <= 0 || ball.X + ball.Radius >= _data.Width)
            {
                ball.XVelocity = -ball.XVelocity;
            }
            if (ball.Y - ball.Radius <= 0 || ball.Y + ball.Radius >= _data.Height)
            {
                ball.YVelocity = -ball.YVelocity;
            }
        }

        private void BallCollision(IBall ball)
        {
            foreach (IBall otherBall in _data.GetAllBalls())
            {
                if (ball != otherBall)
                {
                    float distance = (float)Math.Sqrt(Math.Pow(ball.X - otherBall.X, 2) + Math.Pow(ball.Y - otherBall.Y, 2));
                    if (distance <= ball.Radius + otherBall.Radius)
                    {
                        float tempX = ball.XVelocity;
                        float tempY = ball.YVelocity;
                        ball.XVelocity = otherBall.XVelocity;
                        ball.YVelocity = otherBall.YVelocity;
                        otherBall.XVelocity = tempX;
                        otherBall.YVelocity = tempY;
                    }
                }
            }
        }

        public override IDisposable Subscribe(IObserver<LogicAbstractAPI> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<LogicAbstractAPI>> _observers;
            private IObserver<LogicAbstractAPI> _observer;

            public Unsubscriber(List<IObserver<LogicAbstractAPI>> observers, IObserver<LogicAbstractAPI> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
