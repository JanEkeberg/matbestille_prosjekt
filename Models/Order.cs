using MatBestille.Enums;

namespace MatBestille.Models
{
    public class Order
    {
        private static int OrderCounter = 1;

        public string OrderId { get; private set; }
        public Customer Customer { get; private set; }
        public string RoomNumber { get; private set; }
        public DateTime DeliveryTime { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderLine> OrderLines { get; private set; }

        protected Order() { }

        public Order(Customer customer, string roomNumber, DateTime deliveryTime)
        {
            OrderId = $"O{OrderCounter:D3}";
            OrderCounter++;

            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            RoomNumber = ValidateRoomNumber(roomNumber);
            DeliveryTime = ValidateDeliveryTime(deliveryTime);
            Status = OrderStatus.Pending;
            OrderLines = new List<OrderLine>();
        }

        public string GetInfo()
        {
            return $"Order ID: {OrderId}, Customer: {Customer.Name} {Customer.Surname}, Room: {RoomNumber}, Delivery Time: {DeliveryTime}, Status: {Status}, Total Price: {TotalPrice()} NOK";
        }

        public void DisplayOrderInfo()
        {
            Console.WriteLine(GetInfo());

            if (OrderLines.Count == 0)
            {
                Console.WriteLine("No products in this order.");
                return;
            }

            Console.WriteLine("Order lines:");
            foreach (var line in OrderLines)
            {
                Console.WriteLine($"- {line.GetInfo()}");
            }
        }

        public void AddOrderLine(Produkt product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            quantity = ValidatePositiveNumber(quantity, "Quantity");

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProduktId == product.ProduktId);

            if (existingLine != null)
            {
                existingLine.UpdateQuantity(existingLine.Quantity + quantity);
            }
            else
            {
                OrderLines.Add(new OrderLine(product, quantity));
            }
        }

        public void UpdateOrderLineQuantity(Produkt product, int newQuantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            newQuantity = ValidatePositiveNumber(newQuantity, "Quantity");

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProduktId == product.ProduktId);

            if (existingLine == null)
                throw new ArgumentException("Order line was not found.");

            existingLine.UpdateQuantity(newQuantity);
        }

        public void RemoveOrderLine(Produkt product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProduktId == product.ProduktId);

            if (existingLine == null)
                throw new ArgumentException("Order line was not found.");

            OrderLines.Remove(existingLine);
        }

        public decimal TotalPrice()
        {
            return OrderLines.Sum(x => x.LineTotal);
        }

        public void UpdateRoomNumber(string newRoomNumber)
        {
            RoomNumber = ValidateRoomNumber(newRoomNumber);
        }

        public void UpdateDeliveryTime(DateTime newDeliveryTime)
        {
            DeliveryTime = ValidateDeliveryTime(newDeliveryTime);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        protected static string ValidateRoomNumber(string roomNumber)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new ArgumentException("RoomNumber cannot be empty.");

            return roomNumber.Trim().ToUpper();
        }

        protected static DateTime ValidateDeliveryTime(DateTime deliveryTime)
        {
            if (deliveryTime <= DateTime.Now)
                throw new ArgumentException("Delivery time must be in the future.");

            return deliveryTime;
        }

        protected static int ValidatePositiveNumber(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentException($"{fieldName} must be greater than 0.");

            return value;
        }
    }
}