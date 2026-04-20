namespace MatBestille.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepo;

    
    public OrderService(IRepository<Order> orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public Order CreateOrder(
        string customerId, string roomNumber,
        DateTime deliveryTime, List<OrderLine> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException(
                "Bestillingen kan ikke være tom");

        var order = new Order(
            customerId, roomNumber, deliveryTime);
        order.Lines = lines;

        _orderRepo.Add(order);
        return order;
    }

    public List<Order> GetOrdersByCustomer(string customerId)
    {
        return _orderRepo.GetAll()
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.DeliveryTime)
            .ToList();
    }

    public List<Order> GetAllOrders()
        => _orderRepo.GetAll();

    public List<Order> GetUpcomingOrders()
    {
        return _orderRepo.GetAll()
            .Where(o => o.DeliveryTime > DateTime.Now)
            .OrderBy(o => o.DeliveryTime)  
            .ToList();
    }

    public void MarkAsDelivered(string orderId)
    {
        var order = _orderRepo.GetById(orderId);
        if (order == null)
            throw new Exception("Bestilling ikke funnet");

        order.Status = OrderStatus.Delivered;
        _orderRepo.Update(order);
    }
}