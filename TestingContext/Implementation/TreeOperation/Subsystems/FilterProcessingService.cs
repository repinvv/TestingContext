namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;
    using static NodeReorderingService;

    internal static class FilterProcessingService
    {
        public static List<IFilter> GetTreeFilters(TokenStore store)
        {
            return store.FilterRegistrations
                .Select(x => x.GetFilter())
                .OrderByDescending(x => x.FilterInfo.Priority)
                .ThenBy(x => x.FilterInfo.Id)
                .ToList();
        }

        public static List<ExistsFilter> CreateExistsFiltersForGroups(Tree tree, IEnumerable<IFilter> filters)
        {
            return filters.OfType<IFilterGroup>()
                          .Select(tree.GetNode)
                          .Where(x => x != null)
                          .Select(x => x.CreateExistsFilter())
                          .ToList();
        }

        public static void ReorderNodesForFilters(TreeContext context)
        {
            context.Filters.ForEach(x => x.ForDependencies((dep1, dep2) => ReorderNodes(x, context.Tree, dep1, dep2)));
        }

        public static void GetFinalFilters(TreeContext context)
        {
            context.Filters = GetFinalFilters(context, context.Filters);
        }

        private static List<IFilter> GetFinalFilters(Tree tree, IEnumerable<IFilter> filters)
        {
            return filters.Where(x => x.Dependencies.Any())
                          .Select(filter => GetFinalFilter(tree, filter))
                          .ToList();
        }

        public static IFilter GetFinalFilter(TreeContext context, IFilter filter )
        {
            var inversionDiag = context.IsCvFilter(filter)
                ? tree.Store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : tree.Store.FilterInversions.SafeGet(filter.FilterInfo.FilterToken);
            return inversionDiag != null ? new Inverter(filter, new FilterInfo(tree.Store.NextId, inversionDiag)) : filter;
        }
    }
}
