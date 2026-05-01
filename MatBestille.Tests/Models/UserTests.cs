using MatBestille.Models;
using Xunit;

namespace MatBestille.Tests.Models;

public class UserTests
{
    // Tests that a customer is created with valid customer-specific information.
    [Fact]
    public void Customer_ShouldBeCreatedWithValidData()
    {
        var customer = new Customer(
            "Aashish",
            "Karki",
            "aashish@test.com",
            "12345678",
            "123456789",
            "A045");

        Assert.Equal("Aashish", customer.Name);
        Assert.Equal("Karki", customer.Surname);
        Assert.Equal("aashish@test.com", customer.Email);
        Assert.Equal("12345678", customer.TelNumber);
        Assert.Equal("123456789", customer.OrgNumber);
        Assert.Equal("A045", customer.TeamNumber);
        Assert.Equal("Customer", customer.GetRole());
        Assert.False(customer.IsAdmin);
    }

    // Tests that an admin user gets the correct role and admin access flag.
    [Fact]
    public void Admin_ShouldBeCreatedWithAdminRole()
    {
        var admin = new Admin("Admin", "User", "admin@test.com", "12345678");

        Assert.Equal("Admin", admin.GetRole());
        Assert.True(admin.IsAdmin);
        Assert.Equal("admin@test.com", admin.Email);
    }

    // Tests that an employee user gets the correct role without admin access.
    [Fact]
    public void Employee_ShouldBeCreatedWithEmployeeRole()
    {
        var employee = new Employee("Test", "Employee", "employee@test.com", "87654321");

        Assert.Equal("Employee", employee.GetRole());
        Assert.False(employee.IsAdmin);
        Assert.Equal("employee@test.com", employee.Email);
    }

    // Tests that a customer cannot be created with an invalid email address.
    [Fact]
    public void Customer_ShouldRejectInvalidEmail()
    {
        Assert.Throws<ArgumentException>(() =>
            new Customer(
                "Aashish",
                "Karki",
                "wrong-email",
                "12345678",
                "123456789",
                "A045"));
    }
}
