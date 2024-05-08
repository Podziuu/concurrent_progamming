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
        private float _xVelocity;
        private float _yVelocity;
        private readonly object _positionLock = new object();
        private readonly object _velocityLock = new object();
        private readonly int _radius;
        private bool _isMoving;
        List<IObserver<IBall>> _observers;

        public Ball(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _xVelocity = 3;
            _yVelocity = 3;
            _radius = radius;
            _observers = new List<IObserver<IBall>>();
            //foreach (var observer in _observers)
            //{
            //    observer.OnNext(this);
            //}
        }

        public override float X
        {
            get
            {
                return _x;
            }
        }

        public override float Y
        {
            get 
            {
                return _y;
            }
        }

        public override float XVelocity 
        {
            get { return _xVelocity; }
            set
            { 
                _xVelocity = value;
            } 
        }
        public override float YVelocity
        {
            get { return _yVelocity; }
            set
            {
                _yVelocity = value;
            }
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

        //public override Task Move()
        //{
        //    // send information to logic
        //    foreach (var observer in _observers)
        //    {
        //        observer.OnNext(this);
        //    }
        //    throw new NotImplementedException();
        //}

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        public override async Task Move()
        {
            Random rand = new Random();
         
            _isMoving = true;
            while (_isMoving)
            {
                _x += _xVelocity;
                _y += _yVelocity;
                await Task.Delay(20);

                foreach (var observer in _observers)
                {
                    observer.OnNext(this);
                }

                //float xDiff = targetX - _x;
                //float yDiff = targetY - _y;

                //double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                //int steps = (int)Math.Ceiling(distance / velocity);

                //double xStep = xDiff / steps;
                //double yStep = yDiff / steps;

                //for (int i = 0; i < steps; i++)
                //{
                //    await Task.Delay(20);
                //    X += (float)xStep;
                //    Y += (float)yStep;
                //    OnPropertyChanged(nameof(X));
                //    OnPropertyChanged(nameof(Y));
                //}
            }
        }
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

