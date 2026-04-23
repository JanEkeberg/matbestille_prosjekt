namespace MatBestille.Models
{
    public class Fruits : Produkt
    {
        public int ForHowManyPeople { get; private set; }

        public Fruits(string name, decimal price, int forHowManyPeople) : base(name, price)
        {
            ForHowManyPeople = ValidatePositiveNumber(forHowManyPeople, "Number of servings");
        }

        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Serves: {ForHowManyPeople}");
        }
    }
}