namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static Subsystems.ValidationService;
    using static Subsystems.FilterAssignmentService;
    using static Subsystems.NodeReorderingService;
    using static Subsystems.ProhibitedRelationsService;
    using static Subsystems.TreeBuilder;

    internal static class TreeOperationService
    {
        public static IResolutionContext GetTreeRoot(RegistrationStore store, TestingContext rootSource)
        {
            if (store.Tree != null)
            {
                return store.Tree.RootContext;
            }

            store.Tree = CreateTree(store);
            return store.Tree.RootContext =
                new ResolutionContext<TestingContext>(rootSource, store.RootDefinition, store.Tree.Root, null);
        }

        private static Tree CreateTree(RegistrationStore store)
        {
            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootDefinition);
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store, tree)).ToList();
            BuildNodesTree(tree.Root, nodes);
            store.Filters.ForEach(x => FindProhibitedRelations(tree, x));
            store.Filters.ForEach(x => ReorderNodesForFilter(tree, x));
            store.Filters.ForEach(x => ValidateFilter(tree, x));
            store.Filters.ForEach(x => AssignFilter(tree, x));
            ValidateTree(tree);
            return tree;
        }
    }
}
