using Data;

namespace Logic
{
    internal class PoolTable : LogicAbstractAPI, IObserver<IBall>, IObservable<LogicAbstractAPI>
    {
        private DataAbstractAPI _data;
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
                ball.Subscribe(this);
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
            _data.CreateBalls(ballsQuantity, radius);
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

        public virtual void Subscribe(IObservable<IBall> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNext(IBall value)
        {
            // forward data to Model layer
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
            //_balls.Add(value);
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
