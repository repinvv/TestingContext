namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterToken GroupToken { get; }

        IDiagInfo DiagInfo { get; }
    }
}
