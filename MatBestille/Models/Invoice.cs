using System.Text.Json.Serialization;

namespace MatBestille.Models
{
    public class Invoice
    {
        private static int InvoiceCounter = 1;

        [JsonInclude]
        public string InvoiceId { get; private set; } = string.Empty;

        [JsonInclude]
        public Order Order { get; private set; } = null!;

        [JsonInclude]
        public DateTime GeneratedDate { get; private set; }

        public decimal TotalAmount => Order.TotalPrice();

        // Empty constructor used for JSON deserialization.
        public Invoice() { }

        // Creates a new invoice.
        public Invoice(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));

            InvoiceId = $"I{InvoiceCounter:D3}";
            InvoiceCounter++;
            GeneratedDate = DateTime.Now;
        }

        // Returns invoice information.
        public string GetInfo()
        {
            return $"Invoice ID: {InvoiceId}, Order ID: {Order.OrderId}, Generated Date: {GeneratedDate}, Total Amount: {TotalAmount} NOK";
        }

        // Displays invoice information.
        public void DisplayInvoiceInfo()
        {
            Console.WriteLine(GetInfo());
        }
    }
}