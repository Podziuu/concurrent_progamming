namespace Logic
{
    public class Ball
    {
        private float _x;
        private float _y;
        private readonly int _radius;
        
        public Ball(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public float X
        {
            get => _x;
            set => _x = value;
        }

        public float Y
        {
            get => _y;
            set => _y = value;
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

                await Task.Delay(100);
            }
        }

        }
}
