namespace TestingContextCore.OldImplementation.TreeOperation.Subsystems
{
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Nodes;

    internal static class DependencyNodeService
    {
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Definition);
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
