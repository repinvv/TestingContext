namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal static class DependencyNodeService
    {
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            return dependency.IsCollectionDependency
                ? tree.GetNode(dependency.Definition).SourceParent
                : tree.GetNode(dependency.Definition);
        }
    }
}
