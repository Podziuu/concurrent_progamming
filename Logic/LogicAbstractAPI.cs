namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateApi()
        {
            return new PoolTable(550, 300);
        }

        public abstract void CreateBalls(int ballsQuantity, int radius);
        public abstract void StartGame();
        public abstract void StopGame();
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract List<Ball> GetAllBalls();
    }
}
