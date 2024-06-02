using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace Model
{
    internal class ModelBall : IModelBall, INotifyPropertyChanged
    {
        private float _x;
        private float _y;
        private readonly int _radius;

        private float scale = 1.0f;

        public ModelBall(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override float X
        {
            get => _x * scale;
            set {
                _x = value;
                OnPropertyChanged();
            } 
        }

        public override float Y
        {
            get => _y * scale;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public override int Radius
        {
            get => _radius * (int)scale;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
