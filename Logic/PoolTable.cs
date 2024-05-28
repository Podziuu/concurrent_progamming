using Data;
using System.Numerics;

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
                ball.StartMoving();
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
        }

        public override List<IBall> GetAllBalls()
        {
            return _data.GetAllBalls();
        }

        public override List<List<float>> getBallsPosition()
        {
            return _data.getBallsPosition();
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
                    foreach(IBall otherBall in _data.GetAllBalls())
                    {
                        if (ball != otherBall)
                        {
                            BallCollision(ball, otherBall);
                        }
                    }   
                }
            }
            WallCollision(value);
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
        }

        private void WallCollision(IBall ball)
        {
            if (ball.Position.X - 10 <= 0 || ball.Position.X + 10 >= _data.Width)
            {
                ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
            }
            if (ball.Position.Y - 10 <= 0 || ball.Position.Y + 10 >= _data.Height)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        private void BallCollision(IBall ball, IBall otherBall)
        {
            int distance = (int)Math.Sqrt(Math.Pow((ball.Position.X + ball.Velocity.X) - (otherBall.Position.X + otherBall.Velocity.X), 2) + Math.Pow((ball.Position.Y + ball.Velocity.Y) - (otherBall.Position.Y + otherBall.Velocity.Y), 2));

            if (distance < 20)
            {
                float firstX = (2 * otherBall.Velocity.X) / 2;
                float firstY = (2 * otherBall.Velocity.Y) / 2;
                float secondX = (2 * ball.Velocity.X) / 2;
                float secondY = (2 * ball.Velocity.Y) / 2;
                        
                ball.Velocity = new Vector2(firstX, firstY);
                otherBall.Velocity = new Vector2(secondX, secondY);
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
