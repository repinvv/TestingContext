namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using static Subsystems.TreeBuilder;
    using static Subsystems.FilterAssignmentService;

    internal static class TreeOperationService
    {
        public static Tree GetTree(TokenStore store)
        {
            var tree = store.Tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            nodes.ForEach(x => tree.Nodes.Add(x.Token, x));
            tree.Nodes.Add(store.RootToken, tree.Root);
            BuildNodesTree(store, nodes);
            AssignFilters(store);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }
    }
}
