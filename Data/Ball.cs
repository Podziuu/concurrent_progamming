using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall, IObservable<IBall>
    {
        private float _x;
        private float _y;
        private readonly int _radius;
        private bool _isMoving;
        List<IObserver<IBall>> _observers;

        public Ball(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
            _observers = new List<IObserver<IBall>>();
            //foreach (var observer in _observers)
            //{
            //    observer.OnNext(this);
            //}
        }

        public override float X
        {
            get => _x;
            set { _x = value; }
        }

        public override float Y
        {
            get => _y;
            set { _y = value; }
        }

        public override int Radius
        {
            get => _radius;
        }

        public override bool IsMoving
        {
            get => _isMoving;
            set { _isMoving = value; }
        }

        public override Task Move()
        {
            // send information to logic
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
            throw new NotImplementedException();
        }

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        //public override async Task Move(int width, int height)
        //{
        //    Random rand = new Random();
        //    double velocity = 3;
        //    _isMoving = true;
        //    while (_isMoving)
        //    {
        //        float targetX = rand.Next(_radius, width - _radius);
        //        float targetY = rand.Next(_radius, height - _radius);
        //        float xDiff = targetX - _x;
        //        float yDiff = targetY - _y;

        //        double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

        //        int steps = (int)Math.Ceiling(distance / velocity);

        //        double xStep = xDiff / steps;
        //        double yStep = yDiff / steps;

        //        for (int i = 0; i < steps; i++)
        //        {
        //            await Task.Delay(20);
        //            X += (float)xStep;
        //            Y += (float)yStep;
        //            OnPropertyChanged(nameof(X));
        //            OnPropertyChanged(nameof(Y));
        //        }
        //    }
        //}
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<IBall>> _observers;
            private IObserver<IBall> _observer;

            public Unsubscriber(List<IObserver<IBall>> observers, IObserver<IBall> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observers.Contains(_observer) && !(_observer == null))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}

