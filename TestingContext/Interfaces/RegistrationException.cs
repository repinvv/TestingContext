namespace TestingContextCore.Interfaces
{
    using System;

    public class RegistrationException : Exception
    {
        public RegistrationException(string message) : base(message) { }
    }
}
