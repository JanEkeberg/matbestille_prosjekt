using MatBestille.Interfaces;
using MatBestille.Models;

namespace MatBestille.Menysider;

public class CustomerMenu
{
    private readonly Customer             _kunde;
    private readonly IOrderService        _orderService;
    private readonly IRepository<Product> _produktRepo;

    public CustomerMenu(Customer kunde, IOrderService orderService,
                        IRepository<Product> produktRepo)
    {
        _kunde        = kunde;
        _orderService = orderService;
        _produktRepo  = produktRepo;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== KUNDEMENY – {_kunde.Name} ===");
            Console.WriteLine("1. Se produktkatalog");
            Console.WriteLine("2. Ny bestilling");
            Console.WriteLine("3. Mine bestillinger");
            Console.WriteLine("4. Logg ut");
            Console.Write("Velg: ");

            switch (Console.ReadLine())
            {
                case "1": VisProduktliste();  break;
                case "2": NyBestilling();     break;
                case "3": MineOrdre();        break;
                case "4": return;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void VisProduktliste()
    {
        Console.Clear();
        Console.WriteLine("=== PRODUKTKATALOG ===");

        // Polymorfisme i praksis!
        foreach (var produkt in _produktRepo.GetAll())
            produkt.DisplayProduktInfo();

        Console.WriteLine("\nTrykk en tast...");
        Console.ReadKey();
    }

    private void NyBestilling()
    {
        Console.Clear();
        Console.WriteLine("=== NY BESTILLING ===");

        var produkter = _produktRepo.GetAll();
        var ordreLinjer = new List<OrderLine>();

        while (true)
        {
            Console.WriteLine("\nProdukter:");
            for (int i = 0; i < produkter.Count; i++)
                Console.WriteLine($"{i + 1}. {produkter[i].Name} – {produkter[i].Price} NOK");

            Console.WriteLine("0. Ferdig");
            Console.Write("Velg produkt: ");

            if (!int.TryParse(Console.ReadLine(), out int valg)) continue;
            if (valg == 0) break;
            if (valg < 1 || valg > produkter.Count) continue;

            Console.Write("Antall: ");
            if (!int.TryParse(Console.ReadLine(), out int antall) || antall <= 0)
            {
                Console.WriteLine("Ugyldig antall.");
                continue;
            }

            ordreLinjer.Add(new OrderLine(produkter[valg - 1], antall));
            Console.WriteLine("Lagt til!");
        }

        if (ordreLinjer.Count == 0)
        {
            Console.WriteLine("Ingen produkter valgt. Avbryter.");
            Console.ReadKey();
            return;
        }

        Console.Write("Romnummer: ");
        string rom = Console.ReadLine() ?? "";

        DateTime levering = LesLeveringstid();

        try
        {
            var ordre = _orderService.CreateOrder(
                _kunde.UserId, rom, levering, ordreLinjer);

            Console.WriteLine($"\nBestilling opprettet! ID: {ordre.OrderId}");
            Console.WriteLine($"Leveringstid: {ordre.DeliveryTime:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Total: {ordre.TotalPrice():N0} NOK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil: {ex.Message}");
        }

        Console.WriteLine("Trykk en tast...");
        Console.ReadKey();
    }

    private void MineOrdre()
    {
        Console.Clear();
        Console.WriteLine("=== MINE BESTILLINGER ===");

        var ordrer = _orderService.GetOrdersByCustomer(_kunde.UserId);

        if (ordrer.Count == 0)
        {
            Console.WriteLine("Ingen bestillinger funnet.");
        }
        else
        {
            foreach (var o in ordrer)
            {
                Console.WriteLine($"\n[{o.OrderId}] {o.DeliveryTime:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"  Rom: {o.RoomNumber} | Status: {o.Status}");
                Console.WriteLine($"  Total: {o.TotalPrice():N0} NOK");
            }
        }

        Console.WriteLine("\nTrykk en tast...");
        Console.ReadKey();
    }

    private DateTime LesLeveringstid()
    {
        DateTime standardLeveringstid = DateTime.Now.AddHours(1);

        Console.WriteLine($"Standard leveringstid: {standardLeveringstid:dd.MM.yyyy HH:mm}");
        Console.Write("Trykk Enter for standard tid, eller skriv ny tid (dd.MM.yyyy HH:mm): ");

        string input = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(input))
            return standardLeveringstid;

        if (!DateTime.TryParse(input, out DateTime leveringstid))
        {
            Console.WriteLine("Ugyldig datoformat. Standard leveringstid brukes.");
            return standardLeveringstid;
        }

        if (leveringstid <= DateTime.Now)
        {
            Console.WriteLine("Leveringstid kan ikke være i fortiden. Standard leveringstid brukes.");
            return standardLeveringstid;
        }

        return leveringstid;
    }
}