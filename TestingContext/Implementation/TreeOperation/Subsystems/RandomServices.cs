namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal static class RandomServices
    {
        public static bool IsCvFilter(this IFilter filter)
        {
            var dependencies = filter.Dependencies.ToArray();
            return dependencies.Length == 1 && dependencies[0].Type == DependencyType.CollectionValidity;
        }

        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Token);
            switch (dependency.Type)
            {
                case DependencyType.CollectionValidity:
                    return node.Parent;
                case DependencyType.Collection:
                    return node.SourceParent;
                case DependencyType.Single:
                    return node;
            }

            return node;
        }
    }
}
