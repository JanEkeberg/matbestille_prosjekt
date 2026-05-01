namespace MatBestille.Models
{
    public class OrderLine
    {
        public Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal LineTotal => Product.Price * Quantity;

        // Empty constructor used for object creation and JSON deserialization.
        protected OrderLine() { }

        // Creates a new order line with a product and quantity.
        public OrderLine(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = ValidatePositiveNumber(quantity, "Quantity");
        }

        // Returns order line information as a formatted text.
        public string GetInfo()
        {
            return $"Product: {Product.Name}, Quantity: {Quantity}, Line Total: {LineTotal} NOK";
        }

        // Displays order line information in the console.
        public void DisplayOrderLineInfo()
        {
            Console.WriteLine(GetInfo());
        }

        // Updates the quantity of the order line after validating it.
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = ValidatePositiveNumber(newQuantity, "Quantity");
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