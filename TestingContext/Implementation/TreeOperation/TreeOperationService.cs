namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static Subsystems.FilterAssignmentService;
    using static Subsystems.NodeReorderingService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.NonEqualFilteringService;
    using static Subsystems.TreeBuilder;

    internal static class TreeOperationService
    {
        public static Tree GetTree(RegistrationStore store, TestingContext rootSource)
        {
            return store.Tree ?? (store.Tree = CreateTree(store, rootSource));
        }

        private static Tree CreateTree(RegistrationStore store, TestingContext rootSource)
        {
            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootDefinition);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            nodes.ForEach(x => tree.Nodes.Add(x.Definition, x));
            tree.Nodes.Add(store.RootDefinition, tree.Root);
            BuildNodesTree(tree, nodes);
            store.Filters.ForEach(x => ReorderNodes(tree, x));
            store.Filters.ForEach(x => AssignNonEqualFilters(tree, x));
            store.Filters.ForEach(x => AssignFilter(tree, x));
            store.CollectionValidityFilters.ForEach(x => AssignCollectionValidityFilter(tree, x, store));
            tree.RootContext = new ResolutionContext<TestingContext>(rootSource, tree.Root, null);
            return tree;
        }
    }
}
