namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal static class FilterProcessingService
    {
        public static void SetupTreeFilters(Tree tree)
        {
            tree.Filters = tree.Store.Filters.Select(x => x.GetFilter(null)).ToList();
            tree.Filters.ForAllGroups(grp => tree.FilterGroups.GetOrAdd(grp.FilterInfo.Token, () => grp));
        }

        public static void ProcessTreeFilters(Tree tree)
        {
            var filters = tree.Filters;
            var freeFilters = new List<IFilter>();
            filters.ForGroups(grp => grp.ExtractAbsorbedFilters(freeFilters, tree));
            freeFilters.AddRange(tree.CreateExistsFiltersForGroups(filters.Concat(freeFilters).ToList()));
            filters.Concat(freeFilters).ToList().ForAllGroups(grp => grp.ReplaceGroupFiltersWithExists(freeFilters, tree));

            var orderedFilters = filters.OrderByDescending(x => x.FilterInfo.Priority)
                                        .ThenBy(x => x.FilterInfo.Id);
            tree.Filters = GetFinalFilters(tree, orderedFilters.Concat(freeFilters));
            tree.Filters.ForAllGroups(grp => grp.Filters = GetFinalFilters(tree, grp.Filters));
        }

        private static List<IFilter> GetFinalFilters(Tree tree, IEnumerable<IFilter> filters)
        {
            return filters.Where(x => x.Dependencies.Any())
                          .Select(filter => GetFinalFilter(tree, filter))
                          .ToList();
        }

        public static IFilter GetFinalFilter(Tree tree, IFilter filter )
        {
            var inversionDiag = tree.IsCvFilter(filter)
                ? tree.Store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : tree.Store.FilterInversions.SafeGet(filter.FilterInfo.Token);
            return inversionDiag != null ? new Inverter(filter, new FilterInfo(tree.Store.NextId, inversionDiag)) : filter;
        }
    }
}
