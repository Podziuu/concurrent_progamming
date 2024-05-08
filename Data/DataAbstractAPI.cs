using System.ComponentModel;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateApi()
        {
            return new PoolTable(550, 300);
        }
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract List<IBall> CreateBalls(int ballsQuantity, int radius);
        public abstract List<IBall> GetAllBalls();
        public abstract void RemoveBalls();
    }
}
