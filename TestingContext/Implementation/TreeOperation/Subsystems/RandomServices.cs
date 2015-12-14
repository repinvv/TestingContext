namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class RandomServices
    {
        public static bool IsCvFilter(this TokenStore store, IFilter filter)
        {
            return filter.Dependencies.Count() == 1 && store.CvFilters[filter.Dependencies.First().Token] == filter;
        }

        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Token);
            return dependency.Type == DependencyType.Collection
                ? node.SourceParent
                : node;
        }
    }
}
