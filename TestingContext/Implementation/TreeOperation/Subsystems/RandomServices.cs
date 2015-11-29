namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class RandomServices
    {
        public static bool IsCvFilter(this TokenStore store, IFilter filter)
        {
            return store.CvFilters.Contains(filter);
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
