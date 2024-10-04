using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.MyCalendar
{
    /// <summary>
    /// Interaction logic for CalenderPreviewPage.xaml
    /// </summary>
    public partial class CalendarPreviewPage : Page
    {
        public CalendarPreviewPage()
        {
            InitializeComponent();
        }

        private void BackgroundMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            GifBackground.Position = TimeSpan.Zero;
            GifBackground.Play();
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyCalendar());
        }
        private void Button_ClickGameInfo(object sender, RoutedEventArgs e)
        {
            PopUpWindow.IsOpen = true;
        }
    }
}
