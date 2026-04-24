namespace MatBestille.Models
{
    public class Admin : User
    {
        public Admin(
            string name,
            string surname,
            string email,
            string telNumber
        ) : base(name, surname, email, telNumber)
        {
            IsAdmin = true;
        }

        public override string GetRole()
        {
            return "Admin";
        }
    }
}