using System;

namespace PeopleInfoStorage.Exception
{
    internal abstract class PersonException : ArgumentException
    {
        protected PersonException(string message) : base(message)
        {
        }
    }

    internal class NameException : PersonException
    {
        public NameException(string? invalidName)
            : base($"Invalid name: {invalidName}. Name must start from upper case letter and must not be empty")
        {
        }
    }

    internal class EmailException : PersonException
    {
        public EmailException(string? invalidEmail) : base($"Inappropriate representation of email: {invalidEmail}")
        {
        }
    }

    internal class BirthDateException : PersonException
    {
        public BirthDateException(DateTime? invalidBirthDate)
            : base($"Inappropriate birthday date: {invalidBirthDate}. Years must be in range [1:135]")
        {
        }
    }
}