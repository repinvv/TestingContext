namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using static FilterProcessingService;
    using static NodeReorderingService;
    using static TestingContextCore.Implementation.Registrations.StoreExtension;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IHaveDependencies have)
        {
            return have.Dependencies
                       .Select(x => x.GetDependencyNode(tree))
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }

        public static void AssignFilter(TokenStore store, IFilter filter)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }

            var node = GetAssignmentNode(store.Tree, filter);
            AssignFilterToNode(filter, node);
        }

        public static void AssignFilterToNode(IFilter filter, INode node)
        {
            var disablable = new DisablableFilter(filter);
            node.FilterInfo.Group.Filters.Add(disablable);
            var newIndex = node.Tree.FilterIndex.Any() ? (node.Tree.FilterIndex.Values.Max() + 1) : 0;
            node.Tree.FilterIndex[disablable] = newIndex;
        }

        public static void AssignFilters(TokenStore store)
        {
            var freeFilters = new List<IFilter>();
            foreach (var filter in store.Filters)
            {
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store);
                AddFilter(filter, freeFilters, store);
            }

            freeFilters.ForEach(x => ReorderNodes(store, x.Dependencies.ToArray(), x));
            freeFilters.ForEach(x => AssignFilter(store, x));
            store.Tree.ReorderedNodes.ForEach(x=>AssignExistsFilter(x.Item1, x.Item2));
        }

        private static void AssignExistsFilter(INode node, IFilter absorber)
        {
            var filter = CreateExistsFilter(node.Token, absorber, string.Empty, 0, string.Empty);
            AssignFilterToNode(filter, node.Parent);
        }
    }
}
