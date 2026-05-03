using MatBestille.Models;
using MatBestille.Services;
using MatBestille.Menysider;

namespace MatBestille;

class Program
{
    static void Main(string[] args)
    {
        // Opprett Data-mappe hvis den ikke finnes
        Directory.CreateDirectory("Data");

        // 1. Opprett alle repositories
        var produktRepo = new JsonRepository<Product>("Data/products.json");
        var brukerRepo = new JsonRepository<User>("Data/users.json");
        var ordreRepo = new JsonRepository<Order>("Data/orders.json");
        var fakturaRepo = new JsonRepository<Invoice>("Data/invoices.json");

        // 2. Legg inn eksempeldata hvis JSON-filene er tomme
        DataSeeder.Seed(brukerRepo, produktRepo, ordreRepo);

        // 3. Opprett alle services
        var authService = new AuthService(brukerRepo);
        var orderService = new OrderService(ordreRepo, brukerRepo);
        var invoiceService = new InvoiceService(fakturaRepo, ordreRepo, brukerRepo);

        // 4. Opprett hovedmeny og start
        var mainMenu = new MainMenu(
            authService,
            orderService,
            invoiceService,
            produktRepo);

        mainMenu.Show();
    }
}