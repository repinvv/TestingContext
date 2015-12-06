namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContext.Interface;

    internal interface IDepend
    {
        IDiagInfo DiagInfo { get; }

        IEnumerable<IDependency> Dependencies { get; }
    }
}
