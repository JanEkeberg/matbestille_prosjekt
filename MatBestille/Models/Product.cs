using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Baguette), "baguette")]
    [JsonDerivedType(typeof(Wraps), "wraps")]
    [JsonDerivedType(typeof(Kake), "kake")]
    [JsonDerivedType(typeof(Drikker), "drikker")]
    [JsonDerivedType(typeof(Fruits), "fruits")]
    public abstract class Product
    {
        private static int ProductCounter = 1;

        [JsonInclude]
        public string ProductId { get; private set; } = string.Empty;

        [JsonInclude]
        public string Name { get; private set; } = string.Empty;

        [JsonInclude]
        public decimal Price { get; private set; }

        // Empty constructor used for object creation and JSON deserialization.
        protected Product() { }

        // Creates a new product with a generated product ID, name, and price.
        protected Product(string name, decimal price)
        {
            ProductId = $"P{ProductCounter:D3}";
            ProductCounter++;

            Name = ValidateRequired(name, "Name");
            Price = ValidatePrice(price);
        }

        // Returns product information as a formatted text.
        public string GetInfo()
        {
            return $"ID: {ProductId}, Name: {Name}, Price: {Price} NOK";
        }

        // Displays product information for each specific product type.
        public abstract void DisplayProduktInfo();

        // Updates the product name after validating it.
        public void UpdateName(string newName)
        {
            Name = ValidateRequired(newName, "Name");
        }

        // Updates the product price after validating it.
        public void UpdatePrice(decimal newPrice)
        {
            Price = ValidatePrice(newPrice);
        }

        // Validates that a required text value is not empty.
        protected static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }

        // Validates that the price is greater than zero.
        protected static decimal ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            return price;
        }

        // Validates that the given number is greater than zero.
        protected static int ValidatePositiveNumber(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentException($"{fieldName} must be greater than 0.");

            return value;
        }
    }
}