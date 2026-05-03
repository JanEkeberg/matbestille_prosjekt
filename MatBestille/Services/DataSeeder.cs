using MatBestille.Enums;
using MatBestille.Interfaces;
using MatBestille.Models;

namespace MatBestille.Services;

public static class DataSeeder
{
    public static void Seed(
        IRepository<User> userRepo,
        IRepository<Product> productRepo,
        IRepository<Order> orderRepo)
    {
        SeedUsers(userRepo);
        SeedProducts(productRepo);
        SeedOrders(userRepo, productRepo, orderRepo);
    }

    private static void SeedUsers(IRepository<User> userRepo)
    {
        var users = userRepo.GetAll();

        if (!users.Any(u => u.Email.Equals("admin@mat.no", StringComparison.OrdinalIgnoreCase)))
        {
            var admin = new Admin("Admin", "Bruker", "admin@mat.no", "12345678")
            {
                Password = "admin123"
            };

            userRepo.Add(admin);
        }

        if (!users.Any(u => u.Email.Equals("ansatt@mat.no", StringComparison.OrdinalIgnoreCase)))
        {
            var employee = new Employee("Kari", "Ansatt", "ansatt@mat.no", "87654321")
            {
                Password = "ansatt123"
            };

            userRepo.Add(employee);
        }

        if (!users.Any(u => u.Email.Equals("kunde@mat.no", StringComparison.OrdinalIgnoreCase)))
        {
            var customer = new Customer("Ola", "Kunde", "kunde@mat.no", "11223344", "123456789", "A045")
            {
                Password = "kunde123"
            };

            userRepo.Add(customer);
        }
    }

    private static void SeedProducts(IRepository<Product> productRepo)
    {
        if (productRepo.GetAll().Count > 0)
            return;

        productRepo.Add(new Baguette("Kyllingbaguette", "Gluten, egg"));
        productRepo.Add(new Wraps("Vegetarwrap", "Gluten, melk", true));
        productRepo.Add(new Kake("Sjokoladekake", 350m, 10));
        productRepo.Add(new Fruits("Fruktfat", 250m, 8));
        productRepo.Add(new Drikker("Vann", 25m, BottleSize.Small));
        productRepo.Add(new Drikker("Eplejuice", 35m, BottleSize.Large));
    }

    private static void SeedOrders(
        IRepository<User> userRepo,
        IRepository<Product> productRepo,
        IRepository<Order> orderRepo)
    {
        if (orderRepo.GetAll().Count > 0)
            return;

        var customer = userRepo.GetAll()
            .OfType<Customer>()
            .FirstOrDefault(c => c.Email.Equals("kunde@mat.no", StringComparison.OrdinalIgnoreCase));

        var products = productRepo.GetAll();

        if (customer == null || products.Count < 2)
            return;

        DateTime deliveryTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30);

        var order = new Order(customer, "B203", deliveryTime);
        order.AddOrderLine(products[0], 2);
        order.AddOrderLine(products[1], 1);
        order.UpdateStatus(OrderStatus.Confirmed);

        orderRepo.Add(order);
    }
}