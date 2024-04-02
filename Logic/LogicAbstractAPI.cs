namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateApi()
        {
            return new PoolTableManager();
        }

        public abstract void InitiatePoolTable(int width, int height, int ballsQuantity, int ballRadius);
        public abstract void CreateTasks();
        public abstract List<Ball> GetBalls();
        public abstract bool IsActive();
        public abstract void Activate();
        public abstract void Deactivate();
    }
}
