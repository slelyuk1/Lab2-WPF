using System;

namespace UserStorage.Tools
{
    internal abstract class PersonException : ArgumentException
    {
        protected PersonException(string message) : base(message)
        {
        }
    }

    internal class NameException : PersonException
    {
        public NameException()
            : base("Name must start from upper case letter and must not be empty")
        {
        }
    }

    internal class SurnameException : PersonException
    {
        public SurnameException()
            : base("Surname must start from upper case letter and must not be empty")
        {
        }
    }

    internal class EmailException : PersonException
    {
        public EmailException()
            : base("Inappropriate representation of email !")
        {
        }
    }

    internal class AgeException : PersonException
    {
        public AgeException()
            : base("Inappropriate birthday date ! Age must be in range [1:135] !")
        {
        }
    }
}