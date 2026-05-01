namespace MatBestille.Models
{
    public class Employee : User
    {
        // Empty constructor used for object creation and JSON deserialization.
        public Employee() { }

        // Creates a new employee user with the given personal information.
        public Employee(
            string name,
            string surname,
            string email,
            string telNumber
        ) : base(name, surname, email, telNumber)
        {
            IsAdmin = false;
        }

        // Returns the role name for this user type.
        public override string GetRole()
        {
            return "Employee";
        }
    }
}