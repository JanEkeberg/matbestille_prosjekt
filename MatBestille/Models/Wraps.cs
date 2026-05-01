using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class Wraps : Product
    {
        [JsonInclude]
        public string Allergens { get; private set; } = string.Empty;

        [JsonInclude]
        public bool IsGrov { get; private set; }

        // Empty constructor used for object creation and JSON deserialization.
        public Wraps() { }

        // Creates a new wrap product with a fixed price, allergen information, and bread type.
        public Wraps(string name, string allergens, bool isGrov) : base(name, 65m)
        {
            Allergens = ValidateRequired(allergens, "Allergens");
            IsGrov = isGrov;
        }

        // Displays wrap product information including allergens and bread type.
        public override void DisplayProduktInfo()
        {
            string type = IsGrov ? "Grov" : "Ikke grov";
            Console.WriteLine($"{GetInfo()}, Allergens: {Allergens}, Type: {type}");
        }
    }
}