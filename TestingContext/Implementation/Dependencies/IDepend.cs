namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterToken ParentGroupToken { get; }

        IDiagInfo DiagInfo { get; }
    }
}
