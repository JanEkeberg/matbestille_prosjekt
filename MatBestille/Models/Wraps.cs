using System;

namespace MatBestille.Models
{
    public class Wraps : Produkt
    {
        public string Allergens { get; private set; }
        public bool IsGrov { get; private set; }

        public Wraps(string name, string allergens, bool isGrov) : base(name, 65m)
        {
            Allergens = ValidateRequired(allergens, "Allergens");
            IsGrov = isGrov;
        }

        public override void DisplayProduktInfo()
        {
            string type = IsGrov ? "Grov" : "Ikke grov";
            Console.WriteLine($"{GetInfo()}, Allergens: {Allergens}, Type: {type}");
        }
    }
}