namespace MatBestille.Models
{
    public class Admin : User
    {
        // Empty constructor used for object creation and JSON deserialization.
        public Admin() { }

        // Creates a new admin user with the given personal information.
        public Admin(
            string name,
            string surname,
            string email,
            string telNumber
        ) : base(name, surname, email, telNumber)
        {
            IsAdmin = true;
        }

        // Returns the role name for this user type.
        public override string GetRole()
        {
            return "Admin";
        }
    }
}