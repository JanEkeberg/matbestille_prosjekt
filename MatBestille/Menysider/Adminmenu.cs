using MatBestille.Interfaces;
using MatBestille.Models;
namespace MatBestille.Menysider;
public class AdminMenu
{
    private readonly IOrderService        _orderService;
    private readonly IInvoiceService      _invoiceService;
    private readonly IRepository<Product> _produktRepo;

    public AdminMenu(IOrderService orderService,
                     IInvoiceService invoiceService,
                     IRepository<Product> produktRepo)
    {
        _orderService   = orderService;
        _invoiceService = invoiceService;
        _produktRepo    = produktRepo;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ADMIN-MENY ===");
            Console.WriteLine("1. Alle bestillinger");
            Console.WriteLine("2. Generer faktura");
            Console.WriteLine("3. Legg til produkt");
            Console.WriteLine("4. Logg ut");
            Console.Write("Velg: ");

            switch (Console.ReadLine())
            {
                case "1": AlleOrdre();       break;
                case "2": GenererFaktura();  break;
                case "3": LeggTilProdukt();  break;
                case "4": return;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void AlleOrdre()
    {
        Console.Clear();
        Console.WriteLine("=== ALLE BESTILLINGER ===");

        var ordrer = _orderService.GetAllOrders();

        if (ordrer.Count == 0)
        {
            Console.WriteLine("Ingen bestillinger.");
        }
        else
        {
            foreach (var o in ordrer)
            {
                Console.WriteLine($"\n[{o.OrderId}] {o.DeliveryTime:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"  Kunde: {o.Customer.Name} {o.Customer.Surname} | Rom: {o.RoomNumber}");
                Console.WriteLine($"  Status: {o.Status} | Total: {o.TotalPrice():N0} NOK");
            }
        }

        Console.WriteLine("\nTrykk en tast...");
        Console.ReadKey();
    }

    private void GenererFaktura()
    {
        Console.Write("Ordre-ID: ");
        string id = Console.ReadLine() ?? "";

        try
        {
            var faktura = _invoiceService.GenerateInvoice(id);
            _invoiceService.PrintInvoice(faktura);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil: {ex.Message}");
        }

        Console.WriteLine("Trykk en tast...");
        Console.ReadKey();
    }

    private void LeggTilProdukt()
    {
        Console.Clear();
        Console.WriteLine("=== LEGG TIL PRODUKT ===");
        Console.WriteLine("1. Baguette");
        Console.WriteLine("2. Wraps");
        Console.WriteLine("3. Kake");
        Console.Write("Velg type: ");

        string navn, allergen;

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Navn: "); navn = Console.ReadLine() ?? "";
                Console.Write("Allergener: "); allergen = Console.ReadLine() ?? "";
                _produktRepo.Add(new Baguette(navn, allergen));
                break;
            case "2":
                Console.Write("Navn: "); navn = Console.ReadLine() ?? "";
                Console.Write("Allergener: "); allergen = Console.ReadLine() ?? "";
                Console.Write("Grov? (j/n): ");
                bool grov = Console.ReadLine()?.ToLower() == "j";
                _produktRepo.Add(new Wraps(navn, allergen, grov));
                break;
            case "3":
                Console.Write("Navn: "); navn = Console.ReadLine() ?? "";
                Console.Write("Pris: ");
                decimal.TryParse(Console.ReadLine(), out decimal pris);
                Console.Write("Antall stykker: ");
                int.TryParse(Console.ReadLine(), out int antall);
                _produktRepo.Add(new Kake(navn, pris, antall));
                break;
            default:
                Console.WriteLine("Ugyldig valg.");
                Console.ReadKey();
                return;
        }

        Console.WriteLine("Produkt lagt til! Trykk en tast...");
        Console.ReadKey();
    }
}