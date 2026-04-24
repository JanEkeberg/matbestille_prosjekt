namespace MatBestille.Models
{
    public class Invoice
    {
        private static int InvoiceCounter = 1;

        public string InvoiceId { get; private set; }
        public Order Order { get; private set; }
        public DateTime GeneratedDate { get; private set; }
        public decimal TotalAmount => Order.TotalPrice();

        protected Invoice() { }

        public Invoice(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));

            InvoiceId = $"I{InvoiceCounter:D3}";
            InvoiceCounter++;
            GeneratedDate = DateTime.Now;
        }

        public string GetInfo()
        {
            return $"Invoice ID: {InvoiceId}, Order ID: {Order.OrderId}, Generated Date: {GeneratedDate}, Total Amount: {TotalAmount} NOK";
        }

        public void DisplayInvoiceInfo()
        {
            Console.WriteLine(GetInfo());
        }
    }
}