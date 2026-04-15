namespace MatBestille.Models
{
    public class User
    {
        private static int UserCounter = 1;
        public string FirstName {get; private set;}
        public string LastName {get; private set;}
        public string Email {get; private set;}
        public string PhoneNumber {get; private set;}
        protected User() { }

        protected User(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            Id = $"U{UserCounter:D3}";
            UserCounter++;
            FirstName = FirstName;
            LastName = LastName;
            Email = ValidateEmail(Email);
            PhoneNumber = ValidateRequired(PhoneNumber, "Phone Number");
        }

        public string GetInfo()
        {
            return $"{FirstName} {LastName}";
        }

        public abstract string GetRole();

        protected static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }

        protected static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Email is not valid.");

            return email.Trim();
        }

        public void UpdateEmail(string newEmail)
        {
            Email = ValidateEmail(newEmail);
        } 

        public void UpdatePhone(string newPhone)
        {
            PhoneNumber = ValidateRequired(newPhone, "Phone Number");
        }
    }
}