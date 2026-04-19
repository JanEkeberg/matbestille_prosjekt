namespace MatBestille.Models
{
    public class Employee : User
    {
        public Employee(
            string name,
            string surname,
            string email,
            string telNumber
        ) : base(name, surname, email, telNumber)
        {
            IsAdmin = false;
        }

        public override string GetRole()
        {
            return "Employee";
        }
    }
}