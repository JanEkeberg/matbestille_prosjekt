using MatBestille.Models;
using MatBestille.Services;
using MatBestille.Tests.Helper;
using Xunit;

namespace MatBestille.Tests.Services;

public class AuthServiceTests
{
    // Creates a fresh AuthService and fake user repository for each test.
    private static (AuthService authService, FakeRepository<User> userRepo) CreateAuthService()
    {
        var userRepo = new FakeRepository<User>();
        var authService = new AuthService(userRepo);

        return (authService, userRepo);
    }

    // Tests that registration creates a customer and saves it in the repository.
    [Fact]
    public void Register_ShouldCreateCustomerAndSaveUser()
    {
        var (authService, userRepo) = CreateAuthService();

        var customer = authService.Register(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "password123",
            "12345678",
            "123456789",
            "A045");

        Assert.Equal("Aashish", customer.Name);
        Assert.Equal("Karki", customer.Surname);
        Assert.Equal("aashish@test.com", customer.Email);
        Assert.Equal("password123", customer.Password);
        Assert.Equal("Customer", customer.GetRole());
        Assert.Single(userRepo.GetAll());
    }

    // Tests that login returns the correct user when email and password are correct.
    [Fact]
    public void Login_ShouldReturnUser_WhenCredentialsAreCorrect()
    {
        var (authService, _) = CreateAuthService();

        var registeredCustomer = authService.Register(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "password123",
            "12345678",
            "123456789",
            "A045");

        var result = authService.Login("aashish@test.com", "password123");

        Assert.NotNull(result);
        Assert.Equal(registeredCustomer.UserId, result!.UserId);
        Assert.Equal("aashish@test.com", result.Email);
    }

    // Tests that login returns null when the password is wrong.
    [Fact]
    public void Login_ShouldReturnNull_WhenPasswordIsWrong()
    {
        var (authService, _) = CreateAuthService();

        authService.Register(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "password123",
            "12345678",
            "123456789",
            "A045");

        var result = authService.Login("aashish@test.com", "wrong-password");

        Assert.Null(result);
    }

    // Tests that login returns null when the email does not exist.
    [Fact]
    public void Login_ShouldReturnNull_WhenEmailDoesNotExist()
    {
        var (authService, _) = CreateAuthService();

        var result = authService.Login("missing@test.com", "password123");

        Assert.Null(result);
    }

    // Tests that the same email cannot be registered more than once.
    [Fact]
    public void Register_ShouldRejectDuplicateEmail()
    {
        var (authService, _) = CreateAuthService();

        authService.Register(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "password123",
            "12345678",
            "123456789",
            "A045");

        Assert.Throws<Exception>(() =>
            authService.Register(
                "Another",
                "User",
                "aashish@test.com",
                "password456",
                "87654321",
                "987654321",
                "B123"));
    }

    // Tests that EmailExists returns true for an already registered email.
    [Fact]
    public void EmailExists_ShouldReturnTrue_WhenEmailIsRegistered()
    {
        var (authService, _) = CreateAuthService();

        authService.Register(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "password123",
            "12345678",
            "123456789",
            "A045");

        var result = authService.EmailExists("aashish@test.com");

        Assert.True(result);
    }
}
