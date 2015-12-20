namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class FilterProcessingService
    {
        public static void ProcessFilterGroup(IFilterGroup group, List<IFilter> freeFilters, TokenStore store, Tree tree)
        {
            var groupFilters = new List<IFilter>();
            var cvFilters = group.Filters.Where(store.IsCvFilter)
                                 .ToList();
            groupFilters.ForGroups(grp => ProcessFilterGroup(grp, freeFilters, store, tree));
            foreach (var filter in group.Filters)
            {
                var filterIsAbsorbed = cvFilters
                    .Where(x => x != filter)
                    .Any(x => FilterIsAbsorbed(filter, tree.Nodes[x.Dependencies.First().Token], tree));
                AddFilter(filter, filterIsAbsorbed ? freeFilters : groupFilters, store);
            }
            group.Filters = groupFilters;
        }

        private static bool FilterIsAbsorbed(IFilter filter, INode cvNode, Tree tree)
        {
            return filter.Dependencies.Any(x => DependencyIsAbsorbed(x, cvNode, tree));
        }

        private static bool DependencyIsAbsorbed(IDependency dependency, INode cvNode, Tree tree)
        {
            var node = dependency.GetDependencyNode(tree);
            return node == cvNode || node.IsChildOf(cvNode);
        }

        public static void AddFilter(IFilter filter, List<IFilter> filters, TokenStore store)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }

            var inversionDiag = store.IsCvFilter(filter)
                ? store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : store.FilterInversions.SafeGet(filter.FilterInfo.Token);

            filters.Add(inversionDiag != null ? new Inverter(filter, new FilterInfo(inversionDiag)) : filter);
        }

        public static void SetupTreeFilters(TokenStore store, Tree tree)
        {
            tree.Filters = store.Filters.Select(x => x.GetFilter(null)).ToList();
            tree.Filters.ForGroups(grp => tree.FilterGroups.GetOrAdd(grp.FilterInfo.Token, () => grp));
        }

        public static void ProcessTreeFilters(TokenStore store, Tree tree)
        {
            var filters = tree.Filters;
            tree.Filters = new List<IFilter>();
            foreach (var filter in filters.OrderByDescending(x => x.FilterInfo.Priority)
                                          .ThenBy(x => x.FilterInfo.Id))
            {
                AddFilter(filter, tree.Filters, store);
            }

            var freeFilters = new List<IFilter>();
            filters.ForGroups(grp => ProcessFilterGroup(grp, freeFilters, store, tree));
            freeFilters.Reverse();
            tree.Filters.AddRange(freeFilters);
        }
    }
}
