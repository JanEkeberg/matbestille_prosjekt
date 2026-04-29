namespace MatBestille.Models
{
    public abstract class Product
    {
        private static int ProductCounter = 1;

        public string ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        protected Product() { }

        protected Product(string name, decimal price)
        {
            ProductId = $"P{ProductCounter:D3}";
            ProductCounter++;

            Name = ValidateRequired(name, "Name");
            Price = ValidatePrice(price);
        }

        public string GetInfo()
        {
            return $"ID: {ProductId}, Name: {Name}, Price: {Price} NOK";
        }

        public abstract void DisplayProduktInfo();

        public void UpdateName(string newName)
        {
            Name = ValidateRequired(newName, "Name");
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = ValidatePrice(newPrice);
        }

        protected static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }

        protected static decimal ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            return price;
        }

        protected static int ValidatePositiveNumber(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentException($"{fieldName} must be greater than 0.");

            return value;
        }
    }
}