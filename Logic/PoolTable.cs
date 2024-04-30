using Data;

namespace Logic
{
    internal class PoolTable : LogicAbstractAPI
    {
        private DataAbstractAPI _data;

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

    }
}
