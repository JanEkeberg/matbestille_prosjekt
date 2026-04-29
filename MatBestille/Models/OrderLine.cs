namespace MatBestille.Models
{
    public class OrderLine
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal LineTotal => Product.Price * Quantity;

        protected OrderLine() { }

        public OrderLine(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = ValidatePositiveNumber(quantity, "Quantity");
        }

        public string GetInfo()
        {
            return $"Product: {Product.Name}, Quantity: {Quantity}, Line Total: {LineTotal} NOK";
        }

        public void DisplayOrderLineInfo()
        {
            Console.WriteLine(GetInfo());
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = ValidatePositiveNumber(newQuantity, "Quantity");
        }

        protected static int ValidatePositiveNumber(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentException($"{fieldName} must be greater than 0.");

            return value;
        }
    }
}