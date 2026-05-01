using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class Baguette : Product
    {
        [JsonInclude]
        public string Allergen { get; private set; } = string.Empty;

        // Empty constructor used for object creation and JSON deserialization.
        public Baguette() { }

        // Creates a new baguette product with a fixed price and allergen information.
        public Baguette(string name, string allergen) : base(name, 60m)
        {
            Allergen = ValidateRequired(allergen, "Allergen");
        }

        // Displays baguette product information including allergen details.
        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Allergen: {Allergen}");
        }
    }
}