using MatBestille.Enums;

namespace MatBestille.Models
{
    public class Order
    {
        private static int OrderCounter = 1;

        public string OrderId { get; private set; } = string.Empty;
        public Customer Customer { get; private set; } = null!;
        public string RoomNumber { get; private set; } = string.Empty;
        public DateTime DeliveryTime { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderLine> OrderLines { get; private set; } = new();

        // Empty constructor used for object creation and JSON deserialization.
        protected Order() { }

        // Creates a new order with customer, room number, delivery time, and pending status.
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

        // Returns order information as a formatted text.
        public string GetInfo()
        {
            return $"Order ID: {OrderId}, Customer: {Customer.Name} {Customer.Surname}, Room: {RoomNumber}, Delivery Time: {DeliveryTime}, Status: {Status}, Total Price: {TotalPrice()} NOK";
        }

        // Displays order information and all order lines in the console.
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

        // Adds a product to the order or increases quantity if it already exists.
        public void AddOrderLine(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            quantity = ValidatePositiveNumber(quantity, "Quantity");

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);

            if (existingLine != null)
            {
                existingLine.UpdateQuantity(existingLine.Quantity + quantity);
            }
            else
            {
                OrderLines.Add(new OrderLine(product, quantity));
            }
        }

        // Updates the quantity of an existing product in the order.
        public void UpdateOrderLineQuantity(Product product, int newQuantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            newQuantity = ValidatePositiveNumber(newQuantity, "Quantity");

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);

            if (existingLine == null)
                throw new ArgumentException("Order line was not found.");

            existingLine.UpdateQuantity(newQuantity);
        }

        // Removes an existing product line from the order.
        public void RemoveOrderLine(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existingLine = OrderLines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);

            if (existingLine == null)
                throw new ArgumentException("Order line was not found.");

            OrderLines.Remove(existingLine);
        }

        // Calculates the total price of all order lines.
        public decimal TotalPrice()
        {
            return OrderLines.Sum(x => x.LineTotal);
        }

        // Updates the room number after validating it.
        public void UpdateRoomNumber(string newRoomNumber)
        {
            RoomNumber = ValidateRoomNumber(newRoomNumber);
        }

        // Updates the delivery time after validating it.
        public void UpdateDeliveryTime(DateTime newDeliveryTime)
        {
            DeliveryTime = ValidateDeliveryTime(newDeliveryTime);
        }

        // Updates the current status of the order.
        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        // Validates that the room number is not empty.
        protected static string ValidateRoomNumber(string roomNumber)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new ArgumentException("RoomNumber cannot be empty.");

            return roomNumber.Trim().ToUpper();
        }

        // Validates that the delivery time is in the future.
        protected static DateTime ValidateDeliveryTime(DateTime deliveryTime)
        {
            if (deliveryTime <= DateTime.Now)
                throw new ArgumentException("Delivery time must be in the future.");

            return deliveryTime;
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