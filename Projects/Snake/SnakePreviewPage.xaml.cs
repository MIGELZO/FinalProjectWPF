using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.Snake
{

    public partial class SnakePreviewPage : Page
    {
        public SnakePreviewPage()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGame());
        }
    }
}
