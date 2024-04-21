using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class IModelBall
    {
        public static IModelBall CreateBall(float x, float y, int radius)
        {
            return new ModelBall(x, y, radius);
        }
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract int Radius { get; }
        public abstract void UpdateBall(Object s, PropertyChangedEventArgs e);
        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
