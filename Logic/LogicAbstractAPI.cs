namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateApi()
        {
            return new PoolTable(550, 300);
        }

        //public abstract void InitiatePoolTable(int width, int height, int ballsQuantity, int ballRadius);
        public abstract void CreateBalls(int ballsQuantity, int radius);
        public abstract Task StartGame();
        public abstract void StopGame();
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract List<Ball> Balls { get; }
        public abstract List<Ball> GetAllBalls();
    }
}
