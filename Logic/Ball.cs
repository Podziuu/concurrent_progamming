using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        private float _x;
        private float _y;
        private readonly int _radius;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public float X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public float Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged();}
        }

        public int Radius
        {
            get => _radius;
        }

        public async Task Move(float targetX, float targetY, double velocity)
        {
            float xDiff = targetX - _x;
            float yDiff = targetY - _y;

            double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            int steps = (int)Math.Ceiling(distance / velocity);

            double xStep = xDiff / steps;
            double yStep = yDiff / steps;

            for (int i = 0; i < steps; i++)
            {
                X += (float)xStep;
                Y += (float)yStep;

            }
        }

        }
}
