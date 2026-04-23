using MatBestille.Enums;

namespace MatBestille.Models
{
    public class Drikker : Produkt
    {
        public BottleSize Size { get; private set; }

        public Drikker(string name, decimal price, BottleSize size) : base(name, price)
        {
            Size = size;
        }

        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Size: {Size}");
        }
    }
}