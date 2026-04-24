using MatBestille.Interfaces;
using MatBestille.Enums;
using MatBestille.Models;

namespace MatBestille.Services;

public class InvoiceService(
    IRepository<Invoice> invoiceRepo,
    IRepository<Order> orderRepo,
    IRepository<User> userRepo)
    : IInvoiceService
{
    public Invoice GenerateInvoice(string orderId)
    {
        var order = orderRepo.GetById(orderId)
            ?? throw new Exception("Bestilling ikke funnet");

        if (order.Status == OrderStatus.Invoiced)
            throw new Exception("Faktura allerede generert");

        var invoice = new Invoice(order);
        order.UpdateStatus(OrderStatus.Invoiced);
        invoiceRepo.Add(invoice);
        orderRepo.Update(order);
        return invoice;
    }

    public List<Invoice> GetAllInvoices()
        => invoiceRepo.GetAll();

    public void PrintInvoice(Invoice invoice)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"  FAKTURA {invoice.InvoiceId}");
        Console.WriteLine($"  Dato: {invoice.GeneratedDate:dd.MM.yyyy}");
        Console.WriteLine("================================");

        foreach (var linje in invoice.Order.OrderLines)
        {
            string name     = linje.Product.Name;
            int    quantity = linje.Quantity;
            decimal total   = linje.LineTotal;

            Console.WriteLine(
                $"  {name,-25} x{quantity,2} = {total,8:N0} NOK");
        }

        Console.WriteLine("--------------------------------");

        decimal totalAmount = invoice.TotalAmount;
        Console.WriteLine($"  TOTAL: {totalAmount,25:N0} NOK");
        Console.WriteLine("================================");
    }

    public void SendInvoice(string invoiceId)
    {
        var invoice = invoiceRepo.GetById(invoiceId)
            ?? throw new Exception("Faktura ikke funnet");

        var user = userRepo.GetById(invoice.Order.Customer.UserId);

        Console.WriteLine(
            $"Faktura sendt til: {user?.Email ?? "ukjent e-post"}");
    }
}
