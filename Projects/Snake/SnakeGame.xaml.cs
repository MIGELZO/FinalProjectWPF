using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.Snake
{
    public partial class SnakeGame : Page
    {
        public SnakeGame()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
