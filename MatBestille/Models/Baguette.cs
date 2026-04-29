namespace MatBestille.Models
{
    public class Baguette : Product
    {
        public string Allergen { get; private set; }

        public Baguette(string name, string allergen) : base(name, 60m)
        {
            Allergen = ValidateRequired(allergen, "Allergen");
        }

        public override void DisplayProduktInfo()
        {
            Console.WriteLine($"{GetInfo()}, Allergen: {Allergen}");
        }
    }
}