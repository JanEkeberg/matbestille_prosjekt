namespace DefaultNamespace;

public interface IAuthService
{
    //logg inn _returne bruker eller null
    Use? Login(string email, string password);

    //registere ny kunder
    Customer Register(string name, string surname, string email, string password, string tekNumber, string orgNumber,
        string teamNumber);
    
    //sjekk om e-post allerede er registert
    bool EmailExists(string email);
}