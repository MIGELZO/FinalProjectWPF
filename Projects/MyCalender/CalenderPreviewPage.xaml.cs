using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.MyCalender
{
    /// <summary>
    /// Interaction logic for CalenderPreviewPage.xaml
    /// </summary>
    public partial class CalenderPreviewPage : Page
    {
        public CalenderPreviewPage()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyCalender());
        }
    }
}
