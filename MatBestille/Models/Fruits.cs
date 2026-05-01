using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class Fruits : Product
    {
        [JsonInclude]
        public int ForHowManyPeople { get; private set; }

        // Empty constructor used for object creation and JSON deserialization.
        public Fruits() { }

        // Creates a new fruit product with the given name, price, and number of servings.
        public Fruits(string name, decimal price, int forHowManyPeople) : base(name, price)
        {
            ForHowManyPeople = ValidatePositiveNumber(forHowManyPeople, "Number of servings");
        }

        // Displays fruit product information including how many people it serves.
        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Serves: {ForHowManyPeople}");
        }
    }
}