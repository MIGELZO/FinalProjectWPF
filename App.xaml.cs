using FinalProjectWPF.Enums;
using FinalProjectWPF.FileManagment;
using System.ComponentModel;
using System.Windows;

namespace FinalProjectWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        private (int, GameType) _lastGameScore;
        public event PropertyChangedEventHandler PropertyChanged;

        public (int, GameType) LastGameScore
        {
            get { return _lastGameScore; }
            set
            {
                if (_lastGameScore != value)
                {
                    _lastGameScore = value;
                    OnPropertyChanged(nameof(LastGameScore)); // Trigger event
                }
            }
        }
        public int LoggedInUserID { get; set; }
        public dynamic fmGlobal = new FileManager();
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
