using System.Windows;

namespace FinalProjectWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new GameCenterHomePage());
        }
    }
}