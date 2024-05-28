using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall : IObservable<IBall>
    {
        public static IBall CreateBall(Vector2 pos)
        {
            return new Ball(pos);
        }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Velocity { get; set; }
        public abstract bool IsMoving { get; set; }
        public abstract void StartMoving();
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
    }
}
