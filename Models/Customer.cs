namespace MatBestille.Models
{
    public class Customer : User
    {
        public string OrganizationNumber {get; set;}
        public string OrganizationName {get; set;}
        public string TeamNumber {get; set;}

        public Customer() { }
        public Customer(int id, 
                        string firstName, 
                        string lastName, 
                        string email, 
                        string phoneNumber,
                        string organizationNumber,
                        string organizationName,
                        string teamNumber
        ) : base(id, firstName, lastName, email, phoneNumber)
        {
            OrganizationName = organizationName;
            OrganizationNumber = organizationNumber;
            TeamNumber = teamNumber;
        }

        public override string GetRole()
        {
            return "Customer";
        }
    }
}