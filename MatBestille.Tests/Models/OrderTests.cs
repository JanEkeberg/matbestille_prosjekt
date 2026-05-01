using MatBestille.Enums;
using MatBestille.Models;
using Xunit;

namespace MatBestille.Tests.Models;

public class OrderTests
{
    // Creates a valid customer used by the order tests.
    private static Customer CreateCustomer()
    {
        return new Customer(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "12345678",
            "123456789",
            "A045");
    }

    // Tests that a new order starts as pending with no products added yet.
    [Fact]
    public void Order_ShouldStartWithPendingStatus()
    {
        var customer = CreateCustomer();

        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));

        Assert.Equal(customer, order.Customer);
        Assert.Equal("R101", order.RoomNumber);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Empty(order.OrderLines);
    }

    // Tests that adding different products creates order lines and calculates the total price.
    [Fact]
    public void AddOrderLine_ShouldAddMultipleProductsAndCalculateTotalPrice()
    {
        var customer = CreateCustomer();
        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));
        var baguette = new Baguette("Chicken Baguette", "Gluten");
        var wrap = new Wraps("Caesar Wrap", "Egg", true);
        var drink = new Drikker("Cola", 35m, BottleSize.Small);

        order.AddOrderLine(baguette, 2);
        order.AddOrderLine(wrap, 1);
        order.AddOrderLine(drink, 1);

        Assert.Equal(3, order.OrderLines.Count);
        Assert.Equal(220m, order.TotalPrice());
    }

    // Tests that adding the same product twice increases the existing order line quantity.
    [Fact]
    public void AddOrderLine_ShouldIncreaseQuantity_WhenSameProductIsAddedAgain()
    {
        var customer = CreateCustomer();
        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));
        var baguette = new Baguette("Chicken Baguette", "Gluten");

        order.AddOrderLine(baguette, 2);
        order.AddOrderLine(baguette, 3);

        Assert.Single(order.OrderLines);
        Assert.Equal(5, order.OrderLines[0].Quantity);
        Assert.Equal(300m, order.TotalPrice());
    }

    // Tests that an order cannot be created with a delivery time in the past.
    [Fact]
    public void Order_ShouldRejectPastDeliveryTime()
    {
        var customer = CreateCustomer();

        Assert.Throws<ArgumentException>(() =>
            new Order(customer, "R101", DateTime.Now.AddMinutes(-10)));
    }

    // Tests that order status can be changed when the order is handled.
    [Fact]
    public void UpdateStatus_ShouldChangeOrderStatus()
    {
        var customer = CreateCustomer();
        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));

        order.UpdateStatus(OrderStatus.Confirmed);

        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }
}
