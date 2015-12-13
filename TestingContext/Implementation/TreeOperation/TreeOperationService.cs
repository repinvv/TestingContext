namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using static Subsystems.TreeBuilder;
    using static Subsystems.FilterAssignmentService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.NodesCreationService;

    internal static class TreeOperationService
    {
        public static Tree CreateTree(TokenStore store)
        {
            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            tree.Nodes.Add(store.RootToken, tree.Root);
            BuildNodesTree(tree, GetNodesWithDependencies(store, tree));
            AssignFilters(store, tree);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }
    }
}
