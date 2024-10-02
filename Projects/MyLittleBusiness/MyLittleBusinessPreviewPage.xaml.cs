using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FinalProjectWPF.Projects.MyLittleBusiness
{
    /// <summary>
    /// Interaction logic for MyLittleBusinessPreviewPage.xaml
    /// </summary>
    public partial class MyLittleBusinessPreviewPage : Page
    {
        public MyLittleBusinessPreviewPage()
        {
            InitializeComponent();
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyLittleBusiness());
        }
    }
}
