using MatBestille.Enums;
using MatBestille.Services;
using MatBestille.Models;


namespace matbestille_prosjekt.MatBestille
{
	public class App
	{
		public static void MainMenu()
		{
			Console.Clear();
			Console.WriteLine("\u001b[33mVelkommen til Møtemat appen - her kan du bestille mat til din møte\u001b[0m\n");
			Console.WriteLine("1. Møtemat menu");
			Console.WriteLine("2. Om Oss");
			Console.WriteLine("3. Kontakt");
			Console.WriteLine("4. Logg in");
			Console.WriteLine("5. Register ny bruker");
			Console.WriteLine("0. Avslutt");
			
			string choice = Console.ReadLine();
			switch (choice)
			{
				case "1":
					MøtematMenu();
					break;
				case "2":
					OmOss();
					break;
				case "3":
					Kontakt();
					break;
				case "4":
					LoggIn();
					break;
				case "5":
					RegisterNyBruker();
					break;
				case "0":
					Avslutt();
					break;
				default:
					Console.WriteLine("Ugyldig valg - prøv igjen");
					break;
			}
			MainMenu();
		}
		
		private static void MøtematMenu()
		{
			// Kan ikke lage produkter i produkter.json file pga "Unhandled exception. System.NotSupportedException: Deserialization of interface or abstract types is not supported." 
			// .json filer vet ikke på hvilken måte ta imot produkter, også modeler trenger mer JSON vennlig konstruktorer og mindre 'private set' properties eller var det plannen?
			
			
			/* var produktRepository = new JsonRepository<Produkt>("produkter.json");
			var produkter = produktRepository.GetAll();
			if (!produkter.Any())
			{
				produktRepository.Add(new Baguette("Baguette med ost og skinke", "hvete,melk"));
				produkter = produktRepository.GetAll();
			}

			foreach (var produkt in produkter)
			{
				produkt.DisplayProduktInfo();
			}
			*/
			
			// Midlertidig løsning baguette repository i stedet av Produkt
			
			// Add Baguetts
			var baguetteRepository = new JsonRepository<Baguette>("baguetter.json");
			var baguetter = baguetteRepository.GetAll();
			if (!baguetter.Any())
			{
				baguetteRepository.Add(new Baguette("Baguette med ost og skinke", "hvete,melk"));
				baguetteRepository.Add(new Baguette("Baguette med ost", "hvete, melk"));
				baguetteRepository.Add(new Baguette("Baguette med laks og snøfrisk", "hvete, melk, fisk"));
				baguetteRepository.Add(new Baguette("Baguette med halal kylling", "hvete"));
				baguetter = baguetteRepository.GetAll();
			}
			foreach (var baguette in baguetter)
			{
				baguette.DisplayProduktInfo();
			}
			
			// Add Wraps
			var wrapsRepository = new JsonRepository<Wraps>("wraps.json");
			var wraps= wrapsRepository.GetAll();
			if (!wraps.Any())
			{
				wrapsRepository.Add(new Wraps("Wrap med laks og snøfrisk", "hvete,fisk,melk", true));
				wrapsRepository.Add(new Wraps("Wrap med laks og snøfrisk", "hvete,fisk,melk", false));
				wrapsRepository.Add(new Wraps("Wrap med spekkeskinke og ruccola", "hvete", true));
				wrapsRepository.Add(new Wraps("Wrap med spekkeskinke og ruccola", "hvete", false));
				wraps = wrapsRepository.GetAll();
			}
			foreach (var wrap in wraps)
			{
				wrap.DisplayProduktInfo();
			}
			
			// Add Fruits
			var fruitsRepository = new JsonRepository<Fruits>("fruits.json");
			var fruits = fruitsRepository.GetAll();
			if (!fruits.Any())
			{
				fruitsRepository.Add(new Fruits("Liten fat", 160, 8));
				fruitsRepository.Add(new Fruits("Stor fat", 300, 18));
			
				fruits = fruitsRepository.GetAll();
			}
			foreach (var fruit in fruits)
			{
				fruit.DisplayProduktInfo();
			}
			
			// Add Drikker
			var drikkeRepository = new JsonRepository<Drikker>("drikker.json");
			var drikker = drikkeRepository.GetAll();
			if (!drikker.Any())
			{
				drikkeRepository.Add(new Drikker("Cola", 30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Pepsi", 30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Solo",  30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Urge",  30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Vann med kulsyre",30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Vann uten kulsyre",30, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Cola",60, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Solo",60, BottleSize.Small));
				drikkeRepository.Add(new Drikker("Kaffekanne",50,BottleSize.Large));
				drikkeRepository.Add(new Drikker("Tekanne",50,BottleSize.Large));
				drikkeRepository.Add(new Drikker("Vannkaraffel",10,BottleSize.Large));
				
				drikker = drikkeRepository.GetAll();
			}
			foreach (var drikke in drikker)
			{
				drikke.DisplayProduktInfo();
			}
			Console.ReadLine();
		}
		private static void OmOss()
		{
			Console.WriteLine("Vi er Gruppe 13: Asshish, Jan Memet og Weronika");
			Console.WriteLine("Vi kodet ideen for matbestilling applikasjon");
			Console.WriteLine("Applikasjonen er en svar til problemmet med bestilling av mat for møte fra kantinen. Foreløpig skjer det gjennom ,mail og håndskrevte notater.");
			Console.WriteLine("Pga dette er det ikke kontrol over detaljer for levering eller faktura og kantinen glemmer bestillinger eller sender ikke fsktura og taper penger.");
			Console.WriteLine("Begge sider er ikke fornøyde og leter etter en annen løsning. Vi har en svar til det.");
			Console.WriteLine("\n Trykk 'Enter' knappen for å komme seg tilbake til menu");
			Console.ReadLine();
		}

		private static void Kontakt()
		{
			Console.WriteLine("Kontakt:");
			/* Her kommer detaljer for å kontakte admin */
			Console.ReadLine();
			
		}
		private static void LoggIn()
		{
			//add Admin
			var admin = new Admin("Jan Audun", "Pedersen", "JanAudunPedersen@kantine.no", "68758374");
			admin.Password = "1234Jan!";
			var adminRepository = new JsonRepository<Admin>("admin.json");
			adminRepository.Add(admin);
			
			//add Employee
			var employee = new Employee("Lise Marie", "Lærdal", "LiseMarieLaerdal@kantine.no", "87542169");
			employee.Password = "1234Lise!";
			var employeeRepository = new JsonRepository<Employee>("employee.json");
			employeeRepository.Add(employee);
			
			//add 2 basic CUstomers
			var customer1 = new Customer("Aleksander", "Haukaas", "AleksanderHaukaas@kundefirma.no", "45678912",
				"915028576", "A045");
			customer1.Password = "1234Aleksander!";
			var customer2 = new Customer("Sandra", "Lind", "SandraLind@kundefirma.no", "45678912",
				"915028576", "A090");
			customer2.Password = "1234Sandra!";
			var customerRepository = new JsonRepository<Customer>("customer.json");
			customerRepository.Add(customer1);
			customerRepository.Add(customer2);
			
		}

		private static void RegisterNyBruker()
		{
			Console.WriteLine("Register ny bruker");
			Console.ReadLine();

		}

		private static void Avslutt()
		{
			Console.WriteLine("Trykk Enter for å bekrefte");
			Console.ReadLine();
			System.Environment.Exit(1);
		}

	}
}