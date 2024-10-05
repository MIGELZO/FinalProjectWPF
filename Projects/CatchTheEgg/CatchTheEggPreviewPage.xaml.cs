using FinalProjectWPF.Enums;
using FinalProjectWPF.FileManagment;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace FinalProjectWPF.Projects.CatchTheEgg
{

    public partial class CatchTheEggPreviewPage : Page
    {
        FileManager fm;
        int LoggedInUser;
        public ObservableCollection<PlayerScore> GameScoreData { get; set; } = new ObservableCollection<PlayerScore>();
        GameType CatchTheEgg;


        public CatchTheEggPreviewPage()
        {
            InitializeComponent();
            fm = (FileManager)((App)Application.Current).fmGlobal;
            LoggedInUser = ((App)Application.Current).LoggedInUserID;

            GameScoreData = fm.GetAllPlayersHighScores(GameType.CatchTheEgg);
            ScoreBoard.ItemsSource = GameScoreData;
        }

        private void BackgroundMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            GifBackground.Position = TimeSpan.Zero;
            GifBackground.Play();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameCenterHomePage());
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatchTheEggGame());
        }

        private void Button_ClickGameInfo(object sender, RoutedEventArgs e)
        {
            PopUpWindow.IsOpen = true;
        }
    }
}
