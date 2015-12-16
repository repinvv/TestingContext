namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterGroup Group { get; }

        IDiagInfo DiagInfo { get; }
    }
}
