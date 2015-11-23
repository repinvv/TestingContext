namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;

    internal interface IHaveDependencies
    {
        IEnumerable<IDependency> Dependencies { get; }
    }
}
