using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinalProjectWPF.Projects.Snake.Models
{
    class GridCell : Grid
    {
        private Border border;
        private Image image;

        public GridCell()
        {
            border = new Border
            {
                BorderBrush = Brushes.Transparent,

            };
            image = new Image
            {
                Stretch = Stretch.Fill
            };
            border.Child = image;
            Children.Add(border);
        }

        public void SetState(CellState state, Direction dir)
        {
            switch (state)
            {
                case CellState.Empty:
                    image.Source = null;
                    break;
                case CellState.SnakeHead:
                    switch (dir)
                    {
                        case Direction.Up:
                            image.Source = new BitmapImage(new Uri("Accets/snake_head_up.png", UriKind.Relative));
                            break;
                        case Direction.Down:
                            image.Source = new BitmapImage(new Uri("Accets/snake_head_down.png", UriKind.Relative));
                            break;
                        case Direction.Left:
                            image.Source = new BitmapImage(new Uri("Accets/snake_head_left.png", UriKind.Relative));
                            break;
                        case Direction.Right:
                            image.Source = new BitmapImage(new Uri("Accets/snake_head_right.png", UriKind.Relative));
                            break;
                    }
                    break;
                case CellState.SnakeBody:
                    image.Source = new BitmapImage(new Uri("Accets/body.png", UriKind.Relative));
                    break;
                case CellState.Apple:
                    image.Source = new BitmapImage(new Uri("Accets/apple.png", UriKind.Relative));
                    break;
            }
        }
    }
}
