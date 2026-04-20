namespace MatBestille.Interfaces;

public interface IInvoiceService
{
    //opprett faktura fra en bestilling
    Invoice GenerateInvoice(string orderId);
    //hent alle faktura
    List<Invoice> GetAllInvoices();
    //sender faktura på eposten
    void sendInvoice(string invoiceId);
    //skrive ut faktura
    void PrintInvoice(Invoice invoice);
    