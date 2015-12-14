namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using global::TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterGroup Group { get; }

        IDiagInfo DiagInfo { get; }
    }
}
