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
        public ObservableCollection<ModelBall> Balls => _model.GetBalls();

        public MainWindowViewModel()
        {
            _model = Model.ModelAbstractAPI.CreateAPI();
            StartButton = new RelayCommand(StartGame, CanStartGame);
            StopButton = new RelayCommand(StopGame, CanStopGame);
        }

        public String BallsAmmount 
        { 
            get => _BallsAmmount; 
            set { 
                _BallsAmmount = value;
                OnPropertyChanged();
            } 
        }

        private bool CanStartGame()
        {
            return true;
            //return Balls.Count == 0;
        }

        public void StartGame()
        {
            Console.WriteLine(Balls.Count);
            int ballsAmount = int.Parse(BallsAmmount);
            _model.CreateBalls(ballsAmount, 10);
            _model.StartGame();
            OnPropertyChanged("Balls");
        }

        private bool CanStopGame()
        {
            return Balls.Count > 0;
        }

        public void StopGame()
        {
            _model.StopGame();
            OnPropertyChanged("Balls");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
