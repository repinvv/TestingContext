namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;

    internal static class DependencyNodeService
    {
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
