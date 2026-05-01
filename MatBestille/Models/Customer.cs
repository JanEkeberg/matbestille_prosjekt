using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MatBestille.Models
{
    public class Customer : User
    {
        [JsonInclude]
        public string OrgNumber { get; private set; } = string.Empty;

        [JsonInclude]
        public string TeamNumber { get; private set; } = string.Empty;

        // Empty constructor used for object creation and JSON deserialization.
        public Customer() { }

        // Creates a new customer user with organization and team information.
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

        // Returns the role name for this user type.
        public override string GetRole()
        {
            return "Customer";
        }

        // Updates the organization number after validating it.
        public void UpdateOrgNumber(string newOrgNumber)
        {
            OrgNumber = ValidateOrgNumber(newOrgNumber);
        }

        // Updates the team number after validating it.
        public void UpdateTeamNumber(string newTeamNumber)
        {
            TeamNumber = ValidateTeamNumber(newTeamNumber);
        }

        // Validates that the organization number is exactly 9 digits.
        private static string ValidateOrgNumber(string orgNumber)
        {
            if (string.IsNullOrWhiteSpace(orgNumber))
                throw new ArgumentException("OrgNumber cannot be empty.");

            orgNumber = orgNumber.Trim();

            if (!Regex.IsMatch(orgNumber, @"^\d{9}$"))
                throw new ArgumentException("OrgNumber must be exactly 9 digits.");

            return orgNumber;
        }

        // Validates that the team number follows the required format, for example A045.
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