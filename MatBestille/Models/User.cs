using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MatBestille.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Customer), "customer")]
    [JsonDerivedType(typeof(Admin), "admin")]
    [JsonDerivedType(typeof(Employee), "employee")]
    public abstract class User
    {
        private static int UserCounter = 1;

        [JsonInclude]
        public string UserId { get; private set; } = string.Empty;

        [JsonInclude]
        public string Name { get; private set; } = string.Empty;

        [JsonInclude]
        public string Surname { get; private set; } = string.Empty;

        [JsonInclude]
        public string Email { get; private set; } = string.Empty;

        [JsonInclude]
        public string TelNumber { get; private set; } = string.Empty;

        [JsonInclude]
        public bool IsAdmin { get; protected set; }

        private string _password = string.Empty;

        [JsonInclude]
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Passord kan ikke være tomt.");

                _password = value;
            }
        }

        // Empty constructor used for object creation and JSON deserialization.
        protected User() { }

        // Creates a new user with a generated user ID and validated personal information.
        protected User(string name, string surname, string email, string telNumber)
        {
            UserId = $"U{UserCounter:D3}";
            UserCounter++;

            Name = ValidateRequired(name, "Name");
            Surname = ValidateRequired(surname, "Surname");
            Email = ValidateEmail(email);
            TelNumber = ValidateTelNumber(telNumber);
        }

        // Returns user information as a formatted text.
        public string GetInfo()
        {
            return $"{UserId} - {Name} {Surname}, Email: {Email}, Tel: {TelNumber}, Admin: {IsAdmin}";
        }

        // Returns the role name for each specific user type.
        public abstract string GetRole();

        // Updates the user email after validating it.
        public void UpdateEmail(string newEmail)
        {
            Email = ValidateEmail(newEmail);
        }

        // Updates the user telephone number after validating it.
        public void UpdateTelNumber(string newTelNumber)
        {
            TelNumber = ValidateTelNumber(newTelNumber);
        }

        // Updates the user first name after validating it.
        public void UpdateName(string newName)
        {
            Name = ValidateRequired(newName, "Name");
        }

        // Updates the user surname after validating it.
        public void UpdateSurname(string newSurname)
        {
            Surname = ValidateRequired(newSurname, "Surname");
        }

        // Validates that a required text value is not empty.
        protected static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }

        // Validates that the email is not empty and has a basic email format.
        protected static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");

            email = email.Trim();

            if (!email.Contains("@") || !email.Contains("."))
                throw new ArgumentException("Email is not valid.");

            return email;
        }

        // Validates that the telephone number is exactly 8 digits.
        protected static string ValidateTelNumber(string telNumber)
        {
            if (string.IsNullOrWhiteSpace(telNumber))
                throw new ArgumentException("Telephone number cannot be empty.");

            telNumber = telNumber.Trim();

            if (!Regex.IsMatch(telNumber, @"^\d{8}$"))
                throw new ArgumentException("Telephone number must be exactly 8 digits.");

            return telNumber;
        }
    }
}