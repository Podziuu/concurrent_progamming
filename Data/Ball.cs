using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Data.Logger;

namespace Data
{
    internal class Ball : IBall, IObservable<IBall>
    {
        private int _ballId;
        private Vector2 _position;
        private Vector2 _velocity;
        
        private readonly object _positionLock = new object();
        private readonly object _velocityLock = new object();
        private bool _isMoving;
        List<IObserver<IBall>> _observers;
        private readonly Logger _logger;

        public Ball(int id, Vector2 pos, Logger log)
        {
            Random random = new Random();
            _ballId = id;
            _position = pos;
            _logger = log;
            _velocity = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
            _observers = new List<IObserver<IBall>>();
        }

        public override Vector2 Position
        {
            get
            {
                lock (_positionLock)
                {
                    return _position;
                }
            }
        }

        public override int BallId => _ballId;

        public override Vector2 Velocity 
        {
            get
            {
                lock (_velocityLock)
                {
                    return _velocity;
                }
            }
            set
            { 
                lock (_velocityLock)
                {
                    _velocity = value;
                }
            } 
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

        public override void StartMoving()
        {
            Thread thread = new Thread(Move);
            thread.Start();
        }

        private async void Move()
        {
            _isMoving = true;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            float startingTime = 0f;
            while (_isMoving)
            {
                float currentTime = stopwatch.ElapsedMilliseconds;
                float delta = currentTime - startingTime;

                if(delta >= 1f / 60f)
                {
                    lock (_positionLock)
                    {
                        _position += _velocity;
                    }
                    _logger.Log(this);
                    startingTime = currentTime;
                    await Task.Delay((int)delta / 1000);

                    foreach (var observer in _observers)
                    {
                        observer.OnNext(this);
                    }
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

