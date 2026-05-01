using MatBestille.Enums;
using MatBestille.Models;
using MatBestille.Services;
using MatBestille.Tests.Helper;
using Xunit;

namespace MatBestille.Tests.Services;

public class InvoiceServiceTests
{
    // Creates a valid customer used by the invoice service tests.
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

    // Creates a valid order with products used by the invoice service tests.
    private static Order CreateOrder(Customer customer)
    {
        var order = new Order(customer, "R101", DateTime.Now.AddHours(2));
        order.AddOrderLine(new Baguette("Chicken Baguette", "Gluten"), 2);
        order.AddOrderLine(new Wraps("Caesar Wrap", "Egg", true), 1);

        return order;
    }

    // Creates a fresh InvoiceService with fake repositories for each test.
    private static (InvoiceService invoiceService, FakeRepository<Invoice> invoiceRepo, FakeRepository<Order> orderRepo, FakeRepository<User> userRepo) CreateInvoiceService()
    {
        var invoiceRepo = new FakeRepository<Invoice>();
        var orderRepo = new FakeRepository<Order>();
        var userRepo = new FakeRepository<User>();
        var invoiceService = new InvoiceService(invoiceRepo, orderRepo, userRepo);

        return (invoiceService, invoiceRepo, orderRepo, userRepo);
    }

    // Tests that invoice generation creates an invoice and marks the order as invoiced.
    [Fact]
    public void GenerateInvoice_ShouldCreateInvoiceAndMarkOrderAsInvoiced()
    {
        var (invoiceService, invoiceRepo, orderRepo, userRepo) = CreateInvoiceService();
        var customer = CreateCustomer();
        var order = CreateOrder(customer);
        userRepo.Add(customer);
        orderRepo.Add(order);

        var invoice = invoiceService.GenerateInvoice(order.OrderId);

        Assert.Equal(order.OrderId, invoice.Order.OrderId);
        Assert.Equal(185m, invoice.TotalAmount);
        Assert.Equal(OrderStatus.Invoiced, order.Status);
        Assert.Single(invoiceRepo.GetAll());
    }

    // Tests that an order cannot be invoiced more than once.
    [Fact]
    public void GenerateInvoice_ShouldRejectAlreadyInvoicedOrder()
    {
        var (invoiceService, _, orderRepo, _) = CreateInvoiceService();
        var customer = CreateCustomer();
        var order = CreateOrder(customer);
        order.UpdateStatus(OrderStatus.Invoiced);
        orderRepo.Add(order);

        Assert.Throws<Exception>(() => invoiceService.GenerateInvoice(order.OrderId));
    }

    // Tests that GetAllInvoices returns all invoices stored in the repository.
    [Fact]
    public void GetAllInvoices_ShouldReturnAllInvoices()
    {
        var (invoiceService, invoiceRepo, _, _) = CreateInvoiceService();
        var customer = CreateCustomer();
        var firstInvoice = new Invoice(CreateOrder(customer));
        var secondInvoice = new Invoice(CreateOrder(customer));
        invoiceRepo.Add(firstInvoice);
        invoiceRepo.Add(secondInvoice);

        var result = invoiceService.GetAllInvoices();

        Assert.Equal(2, result.Count);
    }

}
