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

        public async Task Move(int width, int height)
        {
            Random rand = new Random();
            double velocity = 3;
            while (true)
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
