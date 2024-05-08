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
        private List<(float X, float Y)> ballPositions = new List<(float X, float Y)>();
        private readonly object _lock = new object();

        public BallManager()
        {
            poolTable = LogicAbstractAPI.CreateApi();
            this.Subscribe(poolTable);
        }

        public override void CreateBalls(int ballsQuantity, int radius)
        {
            _balls.Clear(); // Wyczyść istniejące kule
            ballPositions.Clear(); // Wyczyść poprzednie pozycje
            poolTable.CreateBalls(ballsQuantity, radius);
            List<IBall> balls = poolTable.GetAllBalls();
            for (int i = 0; i < ballsQuantity; i++)
            {
                // Dodaj kulki do _balls na podstawie informacji o pozycji
                _balls.Add(new ModelBall(balls[i].X, balls[i].Y, radius));
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
            // Odczytaj pozycje kul i zaktualizuj listę
            //ballPositions.Clear();
            //foreach (var ball in poolTable.GetAllBalls())
            //{
            //    ballPositions.Add((ball.X, ball.Y));
            //}
            lock (_lock)
            {
                List<IBall> balls = poolTable.GetAllBalls();
                if(balls.Count == _balls.Count)
                {
                    for (int i = 0; i < balls.Count; i++)
                    {
                        _balls[i].X = balls[i].X;
                        _balls[i].Y = balls[i].Y;
                    }
                }
            }
        }

        //public virtual void OnNext(List<IBall> value)
        //{
        //    foreach (IBall ball in value)
        //    {
        //        _balls.Add(new ModelBall(ball.X, ball.Y, ball.Radius));
        //    }
        //}


        //public override ObservableCollection<ModelBall> Balls => _balls;

        //public override ObservableCollection<IModelBall> GetBalls()
        //{
        //    balls.Clear();
        //    foreach (var ball in poolTable.GetAllBalls())
        //    {
        //        ModelBall b = new ModelBall(ball.X, ball.Y, ball.Radius);
        //        balls.Add(b);
        //        ball.PropertyChanged += b.UpdateBall!;
        //    }
        //    return balls;
        //}

        public override ObservableCollection<IModelBall> Balls => _balls;
        

        public override void StartGame()
        {
            poolTable.StartGame();
        }

        public override void StopGame()
        {
            poolTable.StopGame();
        }
    }
}
