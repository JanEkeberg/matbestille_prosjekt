using System.Text.RegularExpressions;

namespace MatBestille.Models
{
    public abstract class User
    {
        private static int UserCounter = 1;

        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string TelNumber { get; private set; }
        public bool IsAdmin { get; protected set; }

        protected User() { }

        protected User(string name, string surname, string email, string telNumber)
        {
            UserId = $"U{UserCounter:D3}";
            UserCounter++;

            Name = ValidateRequired(name, "Name");
            Surname = ValidateRequired(surname, "Surname");
            Email = ValidateEmail(email);
            TelNumber = ValidateTelNumber(telNumber);
        }

        public string GetInfo()
        {
            return $"{UserId} - {Name} {Surname}, Email: {Email}, Tel: {TelNumber}, Admin: {IsAdmin}";
        }

        public abstract string GetRole();

        public void UpdateEmail(string newEmail)
        {
            Email = ValidateEmail(newEmail);
        }

        public void UpdateTelNumber(string newTelNumber)
        {
            TelNumber = ValidateTelNumber(newTelNumber);
        }

        public void UpdateName(string newName)
        {
            Name = ValidateRequired(newName, "Name");
        }

        public void UpdateSurname(string newSurname)
        {
            Surname = ValidateRequired(newSurname, "Surname");
        }

        protected static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} cannot be empty.");

            return value.Trim();
        }

        protected static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");

            email = email.Trim();

            if (!email.Contains("@") || !email.Contains("."))
                throw new ArgumentException("Email is not valid.");

            return email;
        }

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