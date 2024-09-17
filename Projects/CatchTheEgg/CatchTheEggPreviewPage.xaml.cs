using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.CatchTheEgg
{

    public partial class CatchTheEggPreviewPage : Page
    {
        public CatchTheEggPreviewPage()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatchTheEggGame());
        }
    }
}
