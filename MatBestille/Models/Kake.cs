namespace MatBestille.Models
{
    public class Kake : Product
    {
        public int ForHowManyPeople { get; private set; }

        public Kake(string name, decimal price, int forHowManyPeople) : base(name, price)
        {
            ForHowManyPeople = ValidatePositiveNumber(forHowManyPeople, "Number of servings");
        }

        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Serves: {ForHowManyPeople}");
        }
    }
}