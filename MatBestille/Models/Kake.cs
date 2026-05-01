using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class Kake : Product
    {
        [JsonInclude]
        public int ForHowManyPeople { get; private set; }

        // Empty constructor used for object creation and JSON deserialization.
        public Kake() { }

        // Creates a new cake product with the given name, price, and number of servings.
        public Kake(string name, decimal price, int forHowManyPeople) : base(name, price)
        {
            ForHowManyPeople = ValidatePositiveNumber(forHowManyPeople, "Number of servings");
        }

        // Displays cake product information including how many people it serves.
        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Serves: {ForHowManyPeople}");
        }
    }
}