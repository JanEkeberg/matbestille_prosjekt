namespace MatBestille.Models
{
    public class Invoice
    {
        private static int InvoiceCounter = 1;

        public string InvoiceId { get; private set; } = string.Empty;
        public Order Order { get; private set; } = null!;
        public DateTime GeneratedDate { get; private set; }
        public decimal TotalAmount => Order.TotalPrice();

        // Empty constructor used for object creation and JSON deserialization.
        protected Invoice() { }

        // Creates a new invoice for the given order and generates an invoice ID.
        public Invoice(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));

            InvoiceId = $"I{InvoiceCounter:D3}";
            InvoiceCounter++;
            GeneratedDate = DateTime.Now;
        }

        // Returns invoice information as a formatted text.
        public string GetInfo()
        {
            return $"Invoice ID: {InvoiceId}, Order ID: {Order.OrderId}, Generated Date: {GeneratedDate}, Total Amount: {TotalAmount} NOK";
        }

        // Displays invoice information in the console.
        public void DisplayInvoiceInfo()
        {
            Console.WriteLine(GetInfo());
        }
    }
}