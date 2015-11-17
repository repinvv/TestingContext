namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using static TestingContextCore.Implementation.Dependencies.DependencyType;

    internal static class DependencyNodeService
    {
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Definition);
            switch (dependency.Type)
            {
                case CollectionValidity:
                    return node.Parent;
                case Collection:
                    return node.SourceParent;
                case Single:
                    return node;
            }

            return node;
        }
    }
}
