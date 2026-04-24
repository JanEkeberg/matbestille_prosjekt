using MatBestille.Interfaces;
using MatBestille.Enums;
using MatBestille.Models;

namespace MatBestille.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<User>  _userRepo;  

    public OrderService(IRepository<Order> orderRepo, IRepository<User> userRepo)
    {
        _orderRepo = orderRepo;
        _userRepo  = userRepo;
    }

    public Order CreateOrder(
        string customerId, string roomNumber,
        DateTime deliveryTime, List<OrderLine> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Bestillingen kan ikke være tom");

        var customer = _userRepo.GetById(customerId) as Customer
            ?? throw new Exception("Kunde ikke funnet");

        var order = new Order(customer, roomNumber, deliveryTime);

        foreach (var line in lines)
            order.AddOrderLine(line.Product, line.Quantity);

        _orderRepo.Add(order);
        return order;
    }

    public List<Order> GetOrdersByCustomer(string customerId)
    {
        return _orderRepo.GetAll()
            .Where(o => o.Customer.UserId == customerId)
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
        var order = _orderRepo.GetById(orderId)
            ?? throw new Exception("Bestilling ikke funnet");

        order.UpdateStatus(OrderStatus.Delivered);
        _orderRepo.Update(order);
    }
}
