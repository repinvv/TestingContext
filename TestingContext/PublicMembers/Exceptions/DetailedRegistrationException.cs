namespace TestingContextCore.PublicMembers.Exceptions
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public class DetailedRegistrationException : RegistrationException
    {
        public List<Tuple<IToken, IDiagInfo>> DetailedDiagnostics{ get; }

        public DetailedRegistrationException(string message,  List<Tuple<IToken, IDiagInfo>> detailedDiagnostics, IDiagInfo diagInfo = null)
            : base(message, diagInfo)
        {
            DetailedDiagnostics = detailedDiagnostics;
        }
    }
}
