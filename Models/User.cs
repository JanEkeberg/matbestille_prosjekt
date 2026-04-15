namespace MatBestille.Models
{
    public class User
    {
        private static int UserCounter = 1;
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
        protected User() { }

        protected User(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            Id = $"U{UserCounter:D3}";
            UserCounter++;
            FirstName = FirstName;
            LastName = LastName;
            Email = Email;
            PhoneNumber = PhoneNumber;
        }

        public string GetInfo()
        {
            return $"{FirstName} {LastName}";
        }

        public abstract string GetRole();
    }
}