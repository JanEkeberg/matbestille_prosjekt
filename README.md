# MatBestille

## Prosjektbeskrivelse

MatBestille er et konsollbasert matbestillingssystem utviklet i C# .NET. Prosjektet ble laget som en del av gruppeprosjektet i Emne 2.

Systemet er laget for et enkelt kantinebestillingsscenario. Det støtter tre brukerroller:

- Kunde
- Ansatt
- Administrator

Kunder kan logge inn, se produkter, opprette bestillinger og se egne bestillinger. Ansatte kan se kommende bestillinger og markere bestillinger som levert. Administratorer kan se alle bestillinger, legge til produkter og generere fakturaer.

Hovedfokuset i prosjektet er objektorientert programmering, clean code, filhåndtering, unit testing, Git/GitHub og Scrum-inspirert utvikling.

---

## Hovedfunksjoner

- Innlogging med ulike brukerroller
- Kundemeny
- Ansattmeny
- Administratormeny
- Produktkatalog
- Opprettelse av bestillinger
- Håndtering av ordrestatus
- Fakturagenerering
- Lagring i JSON-filer
- Automatisk opprettelse av demodata
- Unit testing med xUnit

---

## Teknologier brukt

- C# .NET
- Console Application
- JSON-filbehandling
- xUnit
- Git
- GitHub
- Visual Studio

---

## Objektorienterte konsepter brukt

Prosjektet viser flere objektorienterte konsepter:

- Klasser og objekter
- Arv
- Innkapsling
- Polymorfisme
- Abstrakte klasser
- Interfaces
- Dependency injection
- Generics
- Collections
- LINQ

Eksempler:

- `User` er en abstrakt baseklasse for `Customer`, `Employee` og `Admin`.
- `Product` er en abstrakt baseklasse for produkttyper som `Baguette`, `Wraps`, `Kake`, `Fruits` og `Drikker`.
- `JsonRepository<T>` er et generisk repository som brukes til å lagre ulike modelltyper i JSON-filer.
- Services bruker interfaces som `IAuthService`, `IOrderService`, `IInvoiceService` og `IRepository<T>`.

---


## Prosjektstruktur

```text
MatBestille/
│
├── Enums/
│   ├── BottleSize.cs
│   └── OrderStatus.cs
│
├── Interfaces/
│   ├── IAuthService.cs
│   ├── IInvoiceService.cs
│   ├── IOrderService.cs
│   └── IRepository.cs
│
├── Menysider/
│   ├── AdminMenu.cs
│   ├── CustomerMenu.cs
│   ├── MainMenu.cs
│   └── StaffMenu.cs
│
├── Models/
│   ├── Admin.cs
│   ├── Baguette.cs
│   ├── Customer.cs
│   ├── Drikker.cs
│   ├── Employee.cs
│   ├── Fruits.cs
│   ├── Invoice.cs
│   ├── Kake.cs
│   ├── Order.cs
│   ├── OrderLine.cs
│   ├── Product.cs
│   ├── User.cs
│   └── Wraps.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── DataSeeder.cs
│   ├── InvoiceService.cs
│   ├── JsonRepository.cs
│   └── OrderService.cs
│
├── Data/
│   ├── users.json
│   ├── products.json
│   ├── orders.json
│   └── invoices.json
│
└── Program.cs

Testprosjektet ligger separat:

```text
MatBestille.Tests/
```

---

## Demobrukere

Systemet inneholder demodata når JSON-filene er tomme.

| Rolle | E-post | Passord |
|---|---|---|
| Admin | `admin@mat.no` | `admin123` |
| Ansatt | `ansatt@mat.no` | `ansatt123` |
| Kunde | `kunde@mat.no` | `kunde123` |

---

## Hvordan kjøre applikasjonen

```bash
dotnet run --project MatBestille
```

Hvis du allerede står inne i hovedprosjektmappen, kan du bruke:

```bash
dotnet run
```

---

## Hvordan teste applikasjonen

Prosjektet inneholder et xUnit-testprosjekt.

Kjør alle tester med:

```bash
dotnet test
```

---

## Datalagring

Applikasjonen lagrer data i JSON-filer inne i `Data`-mappen.

```text
users.json
products.json
orders.json
invoices.json
```

Hvis filene er tomme, oppretter systemet automatisk eksempelbrukere, produkter og bestillinger ved hjelp av `DataSeeder`.

---

## Nullstille demodata

For å nullstille demodata kan JSON-filene settes til:

```json
[]
```

Deretter kan applikasjonen kjøres på nytt.

---

## Kort oppsummering

MatBestille viser hvordan et enkelt matbestillingssystem kan bygges med C# og objektorientert programmering. Prosjektet bruker tydelig rollefordeling, modeller, services, repositories, JSON-lagring og unit testing for å lage en strukturert og testbar konsollapplikasjon.
