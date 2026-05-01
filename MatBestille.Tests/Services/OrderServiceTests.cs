using MatBestille.Enums;
using MatBestille.Models;
using MatBestille.Services;
using MatBestille.Tests.Helper;
using Xunit;

namespace MatBestille.Tests.Services;

public class OrderServiceTests
{
    // Creates a valid customer used by the order service tests.
    private static Customer CreateCustomer(string email = "aashish@test.com")
    {
        return new Customer(
            "Aashish",
            "Karki",
            email,
            "12345678",
            "123456789",
            "A045");
    }

    // Creates a fresh OrderService with fake repositories for each test.
    private static (OrderService orderService, FakeRepository<Order> orderRepo, FakeRepository<User> userRepo) CreateOrderService()
    {
        var orderRepo = new FakeRepository<Order>();
        var userRepo = new FakeRepository<User>();
        var orderService = new OrderService(orderRepo, userRepo);

        return (orderService, orderRepo, userRepo);
    }

    // Tests that the order service creates and saves an order for a valid customer.
    [Fact]
    public void CreateOrder_ShouldCreateAndSaveOrder()
    {
        var (orderService, orderRepo, userRepo) = CreateOrderService();
        var customer = CreateCustomer();
        userRepo.Add(customer);
        var orderLines = new List<OrderLine>
        {
            new(new Baguette("Chicken Baguette", "Gluten"), 2),
            new(new Wraps("Caesar Wrap", "Egg", true), 1)
        };

        var order = orderService.CreateOrder(customer.UserId, "R101", DateTime.Now.AddHours(2), orderLines);

        Assert.Equal(customer.UserId, order.Customer.UserId);
        Assert.Equal("R101", order.RoomNumber);
        Assert.Equal(2, order.OrderLines.Count);
        Assert.Equal(185m, order.TotalPrice());
        Assert.Single(orderRepo.GetAll());
    }

    // Tests that the order service rejects orders with no products.
    [Fact]
    public void CreateOrder_ShouldRejectEmptyOrderLines()
    {
        var (orderService, _, userRepo) = CreateOrderService();
        var customer = CreateCustomer();
        userRepo.Add(customer);

        Assert.Throws<ArgumentException>(() =>
            orderService.CreateOrder(customer.UserId, "R101", DateTime.Now.AddHours(2), new List<OrderLine>()));
    }

    // Tests that marking an order as delivered updates the order status.
    [Fact]
    public void MarkAsDelivered_ShouldChangeOrderStatusToDelivered()
    {
        var (orderService, orderRepo, _) = CreateOrderService();
        var customer = CreateCustomer();
        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));
        orderRepo.Add(order);

        orderService.MarkAsDelivered(order.OrderId);

        Assert.Equal(OrderStatus.Delivered, order.Status);
    }
}
