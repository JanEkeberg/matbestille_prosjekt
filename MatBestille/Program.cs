using MatBestille.Models;
using MatBestille.Interfaces;
using MatBestille.Services;
using MatBestille.Menysider;
using MatBestille.Enums;


namespace MatBestille;

class Program
{
    static void Main(string[] args)
    {
        // Opprett Data-mappe hvis den ikke finnes
        Directory.CreateDirectory("Data");

        // 1. Opprett alle repositories
        var produktRepo  = new JsonRepository<Product>("Data/products.json");
        var brukerRepo   = new JsonRepository<User>("Data/users.json");
        var ordreRepo    = new JsonRepository<Order>("Data/orders.json");
        var fakturaRepo  = new JsonRepository<Invoice>("Data/invoices.json");

        // 2. Opprett alle services
        var authService    = new AuthService(brukerRepo);
        var orderService   = new OrderService(ordreRepo, brukerRepo); 
        var invoiceService = new InvoiceService(fakturaRepo, ordreRepo, brukerRepo);

        // 3. Opprett hovedmeny og start
        var mainMenu = new MainMenu(
            authService,
            orderService,
            invoiceService,
            produktRepo);  

        mainMenu.Show();
    }
}