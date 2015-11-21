namespace TestingContextCore.OldImplementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.Registrations;
    using TestingContextCore.OldImplementation.ResolutionContext;
    using TestingContextCore.OldImplementation.TreeOperation.Subsystems;

    internal static class TreeOperationService
    {
        public static Tree GetTree(RegistrationStore store)
        {
            return store.Tree ?? (store.Tree = CreateTree(store));
        }

        private static Tree CreateTree(RegistrationStore store)
        {
            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootDefinition);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            nodes.ForEach(x => tree.Nodes.Add(x.Definition, x));
            tree.Nodes.Add(store.RootDefinition, tree.Root);
            TreeBuilder.BuildNodesTree(tree, nodes, store);
            FilterAssignmentService.AssignFilters(tree, store);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null);
            return tree;
        }
    }
}
