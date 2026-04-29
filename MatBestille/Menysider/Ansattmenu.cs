using MatBestille.Interfaces;
using MatBestille.Models;
namespace MatBestille.Menysider;

public class StaffMenu
{
    private readonly IOrderService _orderService;

    public StaffMenu(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== KANTINEANSATT-MENY ===");
            Console.WriteLine("1. Dagens bestillinger");
            Console.WriteLine("2. Alle fremtidige bestillinger");
            Console.WriteLine("3. Merk som levert (bonus)");
            Console.WriteLine("4. Logg ut");
            Console.Write("Velg: ");

            switch (Console.ReadLine())
            {
                case "1": DagensOrdre();      break;
                case "2": FremtidigeOrdre();  break;
                case "3": MerkLevert();       break;
                case "4": return;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void DagensOrdre()
    {
        Console.Clear();
        Console.WriteLine("=== DAGENS BESTILLINGER ===");

        var idag = _orderService.GetUpcomingOrders()
            .Where(o => o.DeliveryTime.Date == DateTime.Today)
            .ToList();

        if (idag.Count == 0)
        {
            Console.WriteLine("Ingen bestillinger i dag.");
        }
        else
        {
            foreach (var o in idag)
            {
                Console.WriteLine($"\n[{o.OrderId}] Kl. {o.DeliveryTime:HH:mm} – Rom {o.RoomNumber}");
                Console.WriteLine($"  Status: {o.Status}");
                foreach (var l in o.OrderLines)
                    Console.WriteLine($"  - {l.Product.Name} x{l.Quantity}");
            }
        }

        Console.WriteLine("\nTrykk en tast...");
        Console.ReadKey();
    }

    private void FremtidigeOrdre()
    {
        Console.Clear();
        Console.WriteLine("=== FREMTIDIGE BESTILLINGER ===");

        var ordrer = _orderService.GetUpcomingOrders();

        foreach (var o in ordrer)
        {
            Console.WriteLine($"\n[{o.OrderId}] {o.DeliveryTime:dd.MM.yyyy HH:mm} – Rom {o.RoomNumber}");
            Console.WriteLine($"  Status: {o.Status} | Total: {o.TotalPrice:N0} NOK");
        }

        Console.WriteLine("\nTrykk en tast...");
        Console.ReadKey();
    }

    private void MerkLevert()
    {
        Console.Write("Ordre-ID: ");
        string id = Console.ReadLine() ?? "";

        try
        {
            _orderService.MarkAsDelivered(id);
            Console.WriteLine("Merket som levert!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil: {ex.Message}");
        }

        Console.ReadKey();
    }
}