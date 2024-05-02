using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class PoolTable : DataAbstractAPI
    {
        private readonly int _width;
        private readonly int _height;
        private List<IBall> _balls = new List<IBall>();

        public PoolTable(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public override int Width => _width;

        public override int Height => _height;

        public override void CreateBalls(int ballsQuantity, int radius)
        {
            Random rand = new Random();
            for (int i = 0; i < ballsQuantity; i++)
            {
                _balls.Add(IBall.CreateBall(rand.Next(0, _width), rand.Next(0, _height), radius));

            }
        }

        public override List<IBall> GetAllBalls()
        {
            return _balls;
        }

        public override void RemoveBalls()
        {
            _balls.Clear();
        }
    }
}
