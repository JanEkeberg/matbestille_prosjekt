using MatBestille.Interfaces;
using MatBestille.Enums;
using MatBestille.Models;

namespace MatBestille.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IRepository<Invoice> _invoiceRepo;
    private readonly IRepository<Order>   _orderRepo;
    private readonly IRepository<User>    _userRepo;

    public InvoiceService(
        IRepository<Invoice> invoiceRepo,
        IRepository<Order>   orderRepo,
        IRepository<User>    userRepo)
    {
        _invoiceRepo = invoiceRepo;
        _orderRepo   = orderRepo;
        _userRepo    = userRepo;
    }

    public Invoice GenerateInvoice(string orderId)
    {
        var order = _orderRepo.GetById(orderId)
            ?? throw new Exception("Bestilling ikke funnet");

        if (order.Status == OrderStatus.Invoiced)
            throw new Exception("Faktura allerede generert");

        var invoice = new Invoice(order);
        order.UpdateStatus(OrderStatus.Invoiced);
        _invoiceRepo.Add(invoice);
        _orderRepo.Update(order);
        return invoice;
    }

    public List<Invoice> GetAllInvoices()
        => _invoiceRepo.GetAll();

    public void PrintInvoice(Invoice invoice)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"  FAKTURA {invoice.InvoiceId}");
        Console.WriteLine($"  Dato: {invoice.GeneratedDate:dd.MM.yyyy}");
        Console.WriteLine("================================");

        foreach (var linje in invoice.Order.OrderLines)
        {
            Console.WriteLine(
                $"  {linje.Product.Name,-25} " +
                $"x{linje.Quantity,2} = "     +
                $"{linje.LineTotal,8:N0} NOK");
        }

        Console.WriteLine("--------------------------------");
        Console.WriteLine(
            $"  TOTAL: {invoice.TotalAmount,25:N0} NOK");
        Console.WriteLine("================================");
    }

    public void SendInvoice(string invoiceId)
    {
        var invoice = _invoiceRepo.GetById(invoiceId)
            ?? throw new Exception("Faktura ikke funnet");

        var kunde = _userRepo.GetById(
            invoice.Order.Customer.UserId);

        Console.WriteLine(
            $"Faktura sendt til: {kunde?.Email ?? "ukjent e-post"}");
    }
}
