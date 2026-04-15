namespace MatBestille.Models
{
    public class Admin : User
    {
        public Admin() { }
        public Admin(int id, 
                    string firstName, 
                    string lastName, 
                    string email, 
                    string phoneNumber
        ) : base(id, firstName, lastName, email, phoneNumber) {}

        public override string GetRole()
        {
            return "Admin";
        }
    }
}