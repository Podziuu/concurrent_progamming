using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class BallManager : ModelAbstractAPI, IObserver<LogicAbstractAPI>
    {
        private LogicAbstractAPI poolTable;
        private ObservableCollection<IModelBall> _balls = new ObservableCollection<IModelBall>();
        private IDisposable unsubscriber;
        private readonly object _lock = new object();

        public BallManager()
        {
            poolTable = LogicAbstractAPI.CreateApi();
            this.Subscribe(poolTable);
        }

        public override void CreateBalls(int ballsQuantity, int radius)
        {
            _balls.Clear();
            poolTable.CreateBalls(ballsQuantity, radius);
            var ballPositions = poolTable.getBallsPosition();
            for (int i = 0; i < ballsQuantity; i++)
            {
                _balls.Add(new ModelBall(ballPositions[i][0], ballPositions[i][1], radius));
            }
        }

        public virtual void Subscribe(IObservable<LogicAbstractAPI> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubcribe()
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

        public virtual void OnNext(LogicAbstractAPI value)
        {
            lock (_lock)
            {
                var ballsPositions = poolTable.getBallsPosition();
                if(ballsPositions.Count == _balls.Count)
                {
                    for (int i = 0; i < ballsPositions.Count; i++)
                    {
                        _balls[i].X = ballsPositions[i][0];
                        _balls[i].Y = ballsPositions[i][1];
                    }
                }
            }
        }

        public override ObservableCollection<IModelBall> Balls => _balls;
        

        public override void StartGame()
        {
            poolTable.StartGame();
        }

        public override void StopGame()
        {
            lock (_lock)
            {
                _balls.Clear();
            }
            poolTable.StopGame();
        }
    }
}
