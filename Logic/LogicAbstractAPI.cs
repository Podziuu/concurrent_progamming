using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI : IObservable<LogicAbstractAPI>
    {
        public static LogicAbstractAPI CreateApi(DataAbstractAPI data = null)
        {
            return new PoolTable(data ?? DataAbstractAPI.CreateApi());
        }
        public abstract void StartGame();
        public abstract void StopGame();   
        public abstract List<IBall> GetAllBalls();
        public abstract void CreateBalls(int ballsQuantity, int radius);
        public abstract IDisposable Subscribe(IObserver<LogicAbstractAPI> observer);
    }
}
