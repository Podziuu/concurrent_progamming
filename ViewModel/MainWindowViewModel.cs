using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Model.ModelAbstractAPI _model;
        private String _BallsAmmount = "";
        public RelayCommand StartButton { get; }
        public RelayCommand StopButton { get; }
        public ObservableCollection<ModelBall> Balls => _model.Balls;

        public MainWindowViewModel()
        {
            _model = Model.ModelAbstractAPI.CreateAPI();
            StartButton = new RelayCommand(StartGame); 
            
        }

        public String BallsAmmount 
        { 
            get => _BallsAmmount; 
            set { 
                _BallsAmmount = value;
                OnPropertyChanged();
            } 
        }

        //private bool CanStartGame()
        //{
        //    return BallsAmmount > 0;
        //}

        public void StartGame()
        {
            Console.WriteLine("StartGame");
            int ballsAmount = int.Parse(BallsAmmount);
            Console.WriteLine(ballsAmount);
            _model.CreateBalls(ballsAmount, 10);
            _model.StartGame();
            OnPropertyChanged("Balls");
        }

        //public void CreateBalls(int ballsQuantity, int radius)
        //{
        //    _model.CreateBalls(ballsQuantity, radius);
        //}


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
