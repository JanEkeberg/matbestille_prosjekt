using System.Text.RegularExpressions;

namespace MatBestille.Models
{
    public class Customer : User
    {
        public string OrgNumber { get; private set; }
        public string TeamNumber { get; private set; }

        public Customer(
            string name,
            string surname,
            string email,
            string telNumber,
            string orgNumber,
            string teamNumber
        ) : base(name, surname, email, telNumber)
        {
            OrgNumber = ValidateOrgNumber(orgNumber);
            TeamNumber = ValidateTeamNumber(teamNumber);
            IsAdmin = false;
        }

        public override string GetRole()
        {
            return "Customer";
        }

        public void UpdateOrgNumber(string newOrgNumber)
        {
            OrgNumber = ValidateOrgNumber(newOrgNumber);
        }

        public void UpdateTeamNumber(string newTeamNumber)
        {
            TeamNumber = ValidateTeamNumber(newTeamNumber);
        }

        private static string ValidateOrgNumber(string orgNumber)
        {
            if (string.IsNullOrWhiteSpace(orgNumber))
                throw new ArgumentException("OrgNumber cannot be empty.");

            orgNumber = orgNumber.Trim();

            if (!Regex.IsMatch(orgNumber, @"^\d{9}$"))
                throw new ArgumentException("OrgNumber must be exactly 9 digits.");

            return orgNumber;
        }

        private static string ValidateTeamNumber(string teamNumber)
        {
            if (string.IsNullOrWhiteSpace(teamNumber))
                throw new ArgumentException("TeamNumber cannot be empty.");

            teamNumber = teamNumber.Trim().ToUpper();

            if (!Regex.IsMatch(teamNumber, @"^[A-Z]\d{3}$"))
                throw new ArgumentException("TeamNumber must follow format like A045.");

            return teamNumber;
        }
    }
}