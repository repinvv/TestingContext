namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.ValidationService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.FilterAssignmentService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.NodeReorderingService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.ProhibitedRelationsService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.TreeBuilder;

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
            var tree = new Tree { Root = new RootNode(store.RootDefinition) };
            var nodes = store.Providers.Select(x => Node.CreateNode(x.Key, x.Value, store)).ToList();
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
