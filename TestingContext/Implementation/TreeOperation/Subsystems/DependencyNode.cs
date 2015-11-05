namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal static class DependencyNodeService
    {
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            return dependency.IsCollectionDependency
                ? tree.Nodes[dependency.Definition].SourceParent
                : tree.Nodes[dependency.Definition];
        }
    }
}
