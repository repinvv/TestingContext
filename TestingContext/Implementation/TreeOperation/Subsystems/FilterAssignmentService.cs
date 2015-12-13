namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using static FilterProcessingService;
    using static NodeReorderingService;
    using static TestingContextCore.Implementation.Registrations.StoreExtension;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IDepend depend)
        {
            var nodes = depend.Dependencies.Select(x => x.GetDependencyNode(tree)).ToList();
            if (depend.Group != null)
            {
                nodes.Add(tree.GetNode(depend.Group.GroupToken));
            }

            return nodes.OrderByDescending(x => x.GetParentalChain().Count)
                         .First();
        }

        public static void AssignFilter(Tree tree, IFilter filter)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }
            
            var node = GetAssignmentNode(tree, filter);
            AssignFilterToNode(filter, node);
        }

        public static void AssignFilterToNode(IFilter filter, INode node)
        {
            node.FilterInfo.Group.Filters.Add(filter);
            var newIndex = node.Tree.FilterIndex.Any() ? (node.Tree.FilterIndex.Values.Max() + 1) : 0;
            node.Tree.FilterIndex[filter] = newIndex;
        }

        public static void AssignFilters(TokenStore store, Tree tree)
        {
            var freeFilters = new List<IFilter>();
            ProcessFilterGroups(store.Filters, freeFilters, store, tree);
            var filters = store.Filters.Concat(freeFilters).ToList();
            filters.ForEach(x => ReorderNodes(tree, x));
            filters.ForEach(x => AssignFilter(tree, x));
            tree.NodesToCreateExistsFilter.ForEach(x => AssignExistsFilter(x.Item1, x.Item2));
        }

        private static void AssignExistsFilter(INode node, IDiagInfo diagInfo)
        {
            var filter = CreateExistsFilter(node.Token, null, diagInfo);
            AssignFilterToNode(filter, node.Parent);
        }
    }
}
