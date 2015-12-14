namespace TestingContextCore.PublicMembers.Exceptions
{
    using System;
    using global::TestingContext.LimitedInterface.Diag;

    public class RegistrationException : Exception
    {
        public IDiagInfo Diag { get; set; }

        public RegistrationException(string message, IDiagInfo diag = null, Exception inner = null) : base(message + Environment.NewLine + "Diagnostics: " + diag, inner)
        {
            Diag = diag;
        }
    }
}
