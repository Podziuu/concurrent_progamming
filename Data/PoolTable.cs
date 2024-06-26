﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class PoolTable : DataAbstractAPI
    {
        private readonly int _width;
        private readonly int _height;
        private List<IBall> _balls = new List<IBall>();
        private readonly object ballLock = new object();

        public PoolTable(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public override int Width => _width;

        public override int Height => _height;

        public override List<IBall> CreateBalls(int ballsQuantity, int radius)
        {
            Random rand = new Random();
            for (int i = 0; i < ballsQuantity; i++)
            {
                _balls.Add(IBall.CreateBall(i + 1, new Vector2(rand.Next(0 + radius, _width - radius), rand.Next(0 + radius, _height - radius))));
            }
            return _balls;
        }

        public override List<IBall> GetAllBalls()
        {
            return _balls;
        }

        public override void RemoveBalls()
        {
            _balls.Clear();
        }

        public override List<List<float>> getBallsPosition()
        {
            var ballPositions = new List<List<float>>();
            lock (ballLock)
            {
                foreach (IBall ball in _balls)
                {
                    var ballPosition = new List<float>() { ball.Position.X, ball.Position.Y };
                    ballPositions.Add(ballPosition);
                }
            }
            return ballPositions;
        }
    }
}
