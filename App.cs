namespace matbestille_prosjekt
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
			Console.WriteLine("Har kommer meny");
			Console.ReadLine();
		}
		private static void OmOss()
		{
			Console.WriteLine("Om Oss");
			Console.ReadLine();
		}

		private static void Kontakt()
		{
			Console.WriteLine("Kontakt");
			Console.ReadLine();
			
		}
		private static void LoggIn()
		{
			Console.WriteLine("Logg in");
			Console.ReadLine();
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