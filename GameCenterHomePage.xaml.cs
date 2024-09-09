using FinalProjectWPF.FileManagment;
using FinalProjectWPF.Projects.Snake;
using FinalProjectWPF.UserManagment;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FinalProjectWPF
{
    public partial class GameCenterHomePage : Page
    {
        public GameCenterHomePage()
        {
            FileManager fm = new FileManager();
            User CurrentUser = null;
            DateTime date = new DateTime(2024, 1, 1);
            if (fm.CheckLastLoginUser().LastLogin > date)
            {
                CurrentUser = fm.CheckLastLoginUser();
            }
            else
            {
                Random random = new Random();
                int randomNumber = random.Next(1000, 10000);
                CurrentUser = fm.CreateNewUser("player" + randomNumber.ToString());
            }

            InitializeComponent();
            DataContext = CurrentUser;
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ClockText.Text = DateTime.Now.ToString("HH:mm:ss");
            ClockDate.Text = DateTime.Now.ToString("d");
        }


        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.6;


        }


        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Name: User\nEmail: user@gmail.com\nPhone: 0500000000", "👤Contact Information");
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.AliceBlue;


        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderBrush = Brushes.Transparent;
        }

        private void SnakeApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakePreviewPage());
        }
    }
}
