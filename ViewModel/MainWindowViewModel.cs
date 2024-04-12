using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Model.ModelAbstractAPI _model;
        private int _ballsAmmount;
        public RelayCommand StartButton { get; set; }

        public MainWindowViewModel()
        {
            _model = Model.ModelAbstractAPI.CreateAPI();
            StartButton = new RelayCommand(o => { CreateBalls(BallsAmmount, 10); }, o => CanStartGame() ); 
            
        }

        public int BallsAmmount 
        { 
            get => _ballsAmmount; 
            set { 
                _ballsAmmount = value;
                OnPropertyChanged();
            } 
        }

        private bool CanStartGame()
        {
            return BallsAmmount > 0;
        }

        public void CreateBalls(int ballsQuantity, int radius)
        {
            _model.CreateBalls(ballsQuantity, radius);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
