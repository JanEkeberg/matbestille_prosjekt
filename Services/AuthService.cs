namespace MatBestille.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;

    public AuthService(IRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public User? Login(string email, string password)
    {
        // LINQ: finn bruker med riktig e-post + passord
        return _userRepo.GetAll()
            .FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);
    }

    public Customer Register(
        string name,     string surname,
        string email,    string password,
        string telNumber,string orgNumber,
        string teamNumber)
    {
        if (EmailExists(email))
            throw new Exception(
                "E-post er allerede registrert");

        var kunde = new Customer(
            name, surname, email,
            telNumber, orgNumber, teamNumber)
        {
            Password = password
        };

        _userRepo.Add(kunde);
        return kunde;
    }

    public bool EmailExists(string email)
    {
        return _userRepo.GetAll()
            .Any(u => u.Email == email);
    }
}