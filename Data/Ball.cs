using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall, IObservable<IBall>
    {
        private Vector2 _position;
        private Vector2 _velocity;
        
        private readonly object _positionLock = new object();
        private readonly object _velocityLock = new object();
        private readonly int _radius;
        private readonly int mass = 5;
        private bool _isMoving;
        List<IObserver<IBall>> _observers;

        public Ball(Vector2 pos, int radius)
        {
            Random random = new Random();
            _position = pos;
            _velocity = new Vector2(random.Next(1, 5), random.Next(1, 5));
            //_velocity = new Vector2(0, 0);
            _radius = radius;
            _observers = new List<IObserver<IBall>>();
        }

        public override Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public override Vector2 Velocity 
        {
            get { return _velocity; }
            set
            { 
                lock (_velocityLock)
                {
                    _velocity = value;
                }
            } 
        }
        

        public override int Radius
        {
            get => _radius;
        }

        public override int Mass
        {
            get => mass;
        }

        public override bool IsMoving
        {
            get => _isMoving;
            set { _isMoving = value; }
        }

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
            _isMoving = true;
            while (_isMoving)
            {

                lock (_positionLock)
                {
                    _position += _velocity;
                }
                
                await Task.Delay(20);

                foreach (var observer in _observers)
                {
                    observer.OnNext(this);
                }
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

