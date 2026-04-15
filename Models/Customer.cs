namespace MatBestille.Models
{
    public class Customer : User
    {
        public string OrganizationNumber {get; private set;}
        public string OrganizationName {get; private set;}
        public string TeamNumber {get; private set;}

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
            OrganizationName = ValidateRequired(organizationName, "Organization Name");
            OrganizationNumber = ValidateRequired(organizationNumber, "Organization Number");
            TeamNumber = ValidateRequired(teamNumber, "Team Number");
        }

        public override string GetRole()
        {
            return "Customer";
        }

        public void UpdateTeam(string teamNumber)
        {
            TeamNumber = ValidateRequired(teamNumber, "Team Number");
        }

        public void UpdateOrganizationName(string orgName)
        {
            OrganizationName = ValidateRequired(orgName, "Organization Name");
        }

        public void UpdateOrganizationNumber(string orgNumber)
        {
            OrganizationNumber = ValidateRequired(orgNumber, "Organization Number");
        }
    }
}