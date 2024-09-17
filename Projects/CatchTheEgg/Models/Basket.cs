namespace FinalProjectWPF.Projects.CatchTheEgg.Models
{
    internal class Basket
    {
        public double Position { get; set; }
        public double Size { get; set; }

        public Basket(double position, double size)
        {
            Position = position;
            Size = size;
        }
    }
}
