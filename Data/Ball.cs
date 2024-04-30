using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private float _x;
        private float _y;
        private readonly int _radius;
        private bool _isMoving;

        public override event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Ball(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override float X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public override float Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); }
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

        public override async Task Move(int width, int height)
        {
            Random rand = new Random();
            double velocity = 3;
            _isMoving = true;
            while (_isMoving)
            {
                float targetX = rand.Next(_radius, width - _radius);
                float targetY = rand.Next(_radius, height - _radius);
                float xDiff = targetX - _x;
                float yDiff = targetY - _y;

                double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                int steps = (int)Math.Ceiling(distance / velocity);

                double xStep = xDiff / steps;
                double yStep = yDiff / steps;

                for (int i = 0; i < steps; i++)
                {
                    await Task.Delay(20);
                    X += (float)xStep;
                    Y += (float)yStep;
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                }
            }
        }
    }
}

