namespace TestingContextCore.PublicMembers.Exceptions
{
    using System;
    using System.Collections.Generic;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;

    public class DetailedRegistrationsException : Exception
    {
        public List<Tuple<IToken, IDiagInfo>> DetailedDiagnostics{ get; }

        public DetailedRegistrationsException(string message,  List<Tuple<IToken, IDiagInfo>> detailedDiagnostics)
            : base(message)
        {
            DetailedDiagnostics = detailedDiagnostics;
        }
    }
}
