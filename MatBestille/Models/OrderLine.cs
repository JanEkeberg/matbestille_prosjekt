using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class OrderLine
    {
        [JsonInclude]
        public Product Product { get; private set; } = null!;

        [JsonInclude]
        public int Quantity { get; private set; }

        public decimal LineTotal => Product.Price * Quantity;

        // Empty constructor used for object creation and JSON deserialization.
        public OrderLine() { }

        // Creates a new order line.
        public OrderLine(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = ValidatePositiveNumber(quantity, "Quantity");
        }

        // Returns order line information.
        public string GetInfo()
        {
            return $"Product: {Product.Name}, Quantity: {Quantity}, Line Total: {LineTotal} NOK";
        }

        // Displays order line information.
        public void DisplayOrderLineInfo()
        {
            Console.WriteLine(GetInfo());
        }

        // Updates quantity.
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = ValidatePositiveNumber(newQuantity, "Quantity");
        }

        // Validates positive number.
        protected static int ValidatePositiveNumber(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentException($"{fieldName} must be greater than 0.");

            return value;
        }
    }
}