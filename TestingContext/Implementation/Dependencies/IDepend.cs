namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IDepend
    {
        IEnumerable<IDependency> Dependencies { get; }

        IFilterGroup Group { get; }

        IDiagInfo DiagInfo { get; }
    }
}
