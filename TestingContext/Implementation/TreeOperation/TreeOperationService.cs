namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;

    internal static class TreeOperationService
    {
        public static Tree GetTree(TokenStore store)
        {
            return store.Tree ?? (store.Tree = CreateTree(store));
        }

        private static Tree CreateTree(TokenStore store)
        {
            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            nodes.ForEach(x => tree.Nodes.Add(x.Token, x));
            tree.Nodes.Add(store.RootToken, tree.Root);
            TreeBuilder.BuildNodesTree(tree, nodes, store);
            FilterAssignmentService.AssignFilters(tree, store);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null);
            return tree;
        }
    }
}
