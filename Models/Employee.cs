namespace MatBestille.Models
{
    public class Employee : User
    {
        public Employee() { }
        public Employee(int id, 
                        string firstName, 
                        string lastName, 
                        string email, 
                        string phoneNumber
        ) : base(id, firstName, lastName, email, phoneNumber) {}

        public override string GetRole()
        {
            return "Employee";
        }
    }
}