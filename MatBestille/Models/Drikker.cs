using System.Text.Json.Serialization;
using MatBestille.Enums;

namespace MatBestille.Models
{
    public class Drikker : Product
    {
        [JsonInclude]
        public BottleSize Size { get; private set; }

        // Empty constructor used for object creation and JSON deserialization.
        public Drikker() { }

        // Creates a new drink product with the given name, price, and bottle size.
        public Drikker(string name, decimal price, BottleSize size) : base(name, price)
        {
            Size = size;
        }

        // Displays drink product information including bottle size.
        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Size: {Size}");
        }
    }
}