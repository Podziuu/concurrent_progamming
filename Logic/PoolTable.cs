using Data;

namespace Logic
{
    internal class PoolTable : LogicAbstractAPI, IObserver<IBall>
    {
        private DataAbstractAPI _data;
        private IDisposable unsubscriber;

        public PoolTable(DataAbstractAPI data)
        {
            _data = data;
        }

        public override void StartGame()
        {
            foreach (IBall ball in _data.GetAllBalls())
            {
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
            //_balls.Add(value);
        }
    }
}
