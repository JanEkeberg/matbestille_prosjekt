using MatBestille.Interfaces;
using MatBestille.Models;
namespace MatBestille.Menysider;


public class MainMenu
{
    private readonly IAuthService    _auth;
    private readonly IOrderService   _orderService;
    private readonly IInvoiceService _invoiceService;
    private readonly IRepository<Product> _produktRepo;

    public MainMenu(IAuthService auth, IOrderService orderService,
                    IInvoiceService invoiceService,
                    IRepository<Product> produktRepo)
    {
        _auth           = auth;
        _orderService   = orderService;
        _invoiceService = invoiceService;
        _produktRepo    = produktRepo;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== KANTINEBESTILLING ===");
            Console.WriteLine("1. Logg inn");
            Console.WriteLine("2. Registrer ny bruker");
            Console.WriteLine("3. Avslutt");
            Console.Write("Velg: ");

            switch (Console.ReadLine())
            {
                case "1": LoggInn();   break;
                case "2": Registrer(); break;
                case "3": return;
                default:
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void LoggInn()
    {
        Console.Write("E-post: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Passord: ");
        string passord = Console.ReadLine() ?? "";

        var bruker = _auth.Login(email, passord);

        if (bruker == null)
        {
            Console.WriteLine("Feil e-post eller passord. Trykk en tast...");
            Console.ReadKey();
            return;
        }

        // Ruter til riktig meny basert på brukertype
        if (bruker is Admin)
            new AdminMenu(_orderService, _invoiceService, _produktRepo).Show();
        else if (bruker is Employee)
            new StaffMenu(_orderService).Show();
        else if (bruker is Customer kunde)
            new CustomerMenu(kunde, _orderService, _produktRepo).Show();
    }

    private void Registrer()
    {
        Console.WriteLine("=== REGISTRER NY BRUKER ===");

        try
        {
            Console.Write("Fornavn: ");
            string navn = Console.ReadLine() ?? "";

            Console.Write("Etternavn: ");
            string etternavn = Console.ReadLine() ?? "";

            Console.Write("E-post: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Passord: ");
            string passord = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer: ");
            string tlf = Console.ReadLine() ?? "";

            Console.Write("Organisasjonsnummer (9 siffer): ");
            string orgNr = Console.ReadLine() ?? "";

            Console.Write("Avdelingsnummer (f.eks. A045): ");
            string teamNr = Console.ReadLine() ?? "";

            _auth.Register(navn, etternavn, email, passord, tlf, orgNr, teamNr);

            Console.WriteLine("Bruker registrert! Trykk en tast...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil: {ex.Message}. Trykk en tast...");
        }

        Console.ReadKey();
    }
}