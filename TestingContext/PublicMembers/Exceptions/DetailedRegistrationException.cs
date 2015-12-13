namespace TestingContextCore.PublicMembers.Exceptions
{
    using System;
    using System.Collections.Generic;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;

    public class DetailedRegistrationException : RegistrationException
    {
        public List<Tuple<IToken, IDiagInfo>> DetailedDiagnostics{ get; }

        public DetailedRegistrationException(string message,  List<Tuple<IToken, IDiagInfo>> detailedDiagnostics, IDiagInfo diagInfo)
            : base(message, diagInfo)
        {
            DetailedDiagnostics = detailedDiagnostics;
        }
    }
}
