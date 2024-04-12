using System.Threading.Tasks;

namespace Logic
{
    internal class PoolTableManager : LogicAbstractAPI
    {
        private PoolTable _poolTable;
        private List<Task> _tasks = new List<Task>();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public override void InitiatePoolTable(int width, int height, int ballsQuantity, int ballRadius)
        {
            _poolTable = new PoolTable(width, height);
            _poolTable.CreateBalls(ballsQuantity, ballRadius);
        }

        public override void CreateTasks()
        {
            Random random = new Random();
            Parallel.ForEach(_poolTable.Balls, ball =>
            {
                Task task = new Task(async () =>
                {
                    while (this._poolTable.IsActive)
                    {
                        await _semaphore.WaitAsync();

                        ball.Move(random.Next(-1, 2), random.Next(-1, 2));
                        Thread.Sleep(100);

                        _semaphore.Release();
                    }
                });
                _tasks.Add(task);
            });
        }

        public override List<Ball> GetBalls()
        {
            return this._poolTable.Balls;
        }

        public override bool IsActive()
        {
            return this._poolTable.IsActive;
        }

        public override void Activate()
        {
            this._poolTable.IsActive = true;
            foreach (Task task in _tasks)
            {
                task.Start();
            }
        }

        public override void Deactivate()
        {
            _poolTable.IsActive = false;

            Task.WaitAll(_tasks.ToArray());

            foreach (Task task in _tasks)
            {
                task.Dispose();
            }
            _tasks.Clear();

            _poolTable.Balls.Clear();
        }
    }
}
