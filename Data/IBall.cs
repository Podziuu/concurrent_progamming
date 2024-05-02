using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall
    {
        public static IBall CreateBall(float x, float y, int radius)
        {
            return new Ball(x, y, radius);
        }
        public abstract float X { get; }
        public abstract float Y { get; }
        public abstract float XVelocity { get; set; }
        public abstract float YVelocity { get; set; }
        public abstract int Radius { get; }
        public abstract bool IsMoving { get; set; }
        public abstract Task Move();
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
    }
}
