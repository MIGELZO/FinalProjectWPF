﻿using FinalProjectWPF.Enums;
using FinalProjectWPF.Projects.CatchTheEgg.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FinalProjectWPF.Projects.CatchTheEgg
{
    public partial class CatchTheEggGame : Page
    {
        GameManager gameManager;
        DispatcherTimer GameTimer = new DispatcherTimer();
        ImageBrush basket = new ImageBrush();
        ImageBrush backgroundImage = new ImageBrush();
        Random rand = new Random();

        public CatchTheEggGame()
        {
            InitializeComponent();
            InitializeGameAndScreen();

            SizeChanged += MainWindow_SizeChanged;
        }
        private void InitializeGameAndScreen()
        {
            MyCanvas.Focus();

            basket.ImageSource = new BitmapImage(new Uri("../../../Projects/CatchTheEgg/Accets/Basket.png", UriKind.Relative));
            Basket.Fill = basket;
            MyCanvas.Background = backgroundImage;

            gameManager = new GameManager(MyCanvas, new Basket(Canvas.GetLeft(Basket), Basket.Width));
            DataContext = gameManager.PlayerBasket;

            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(30);
            GameTimer.Start();
        }
        private void GameEngine(object sender, EventArgs e)
        {
            ScoreText.Content = "Score: " + gameManager.Score;
            Hearts.Content = "Hearts: " + gameManager.Hearts;

            double windowHeight = MyCanvas.ActualHeight;
            double basketPosition = windowHeight - 150;
            int basketPositionPoint = (int)basketPosition;
            Canvas.SetTop(Basket, basketPositionPoint);

            double windowWidth = MyCanvas.ActualWidth;
            double heartsposition = windowWidth - 200;
            int heartspositionPoint = (int)heartsposition;
            Canvas.SetLeft(Hearts, heartspositionPoint);
            gameManager.UpdateGame();

            if (gameManager.IsGameOver())
            {
                GameTimer.Stop();
                ((App)Application.Current).LastGameScore = (gameManager.Score, GameType.CatchTheEgg);
                MessageBox.Show($"You Lost!\nYour Score: {gameManager.Score}\nClick to play again", "Catch The Egg");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            gameManager.ResetGameValues();
            ScoreText.Content = "Score: 0";
            Hearts.Content = "Hearts: 5";
            MyCanvas.Focus();
            GameTimer.Start();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            double pX = position.X;
            Canvas.SetLeft(Basket, pX - 40);
            gameManager.PlayerBasket.Position = pX - 40;
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWidth = MyCanvas.ActualWidth;
            double newHeight = MyCanvas.ActualHeight;
            gameManager.PlayerBasket.Size = newWidth * 0.1;
            gameManager.PlayerBasket.Size = newHeight * 0.1;

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            GameTimer.Stop();
            NavigationService.Navigate(new CatchTheEggPreviewPage());
        }
    }
}
