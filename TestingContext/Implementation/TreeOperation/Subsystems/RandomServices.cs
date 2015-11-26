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
            return dependencies.Length == 1 && dependencies[0].Type == DependencyType.Parent;
        }

        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Token);
            switch (dependency.Type)
            {
                case DependencyType.Parent:
                    return node.Parent;
                case DependencyType.SourceParent:
                    return node.SourceParent;
                case DependencyType.Item:
                    return node;
            }

            return node;
        }
    }
}
