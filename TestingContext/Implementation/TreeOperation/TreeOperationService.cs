namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.Resolution;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.TreeBuilder;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.FilterAssignmentService;

    internal static class TreeOperationService
    {
        public static Tree GetTree(TokenStore store)
        {
            return store.Tree ?? CreateTree(store);
        }

        private static Tree CreateTree(TokenStore store)
        {
            var tree = store.Tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            nodes.ForEach(x => tree.Nodes.Add(x.Token, x));
            tree.Nodes.Add(store.RootToken, tree.Root);
            BuildNodesTree(store, nodes);
            AssignFilters(store);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null);
            return tree;
        }
    }
}
