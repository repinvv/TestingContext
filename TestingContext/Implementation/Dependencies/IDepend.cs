namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterToken GroupToken { get; }

        IDiagInfo DiagInfo { get; }
    }
}
