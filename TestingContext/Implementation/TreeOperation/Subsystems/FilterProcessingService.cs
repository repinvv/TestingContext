namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using static NodeReorderingService;

    internal static class FilterProcessingService
    {
        public static void SetupTreeFilters(Tree tree)
        {
            tree.Filters = tree.Store.Filters
                .Select(x => x.GetFilter(null))
                .OrderByDescending(x => x.FilterInfo.Priority)
                .ThenBy(x => x.FilterInfo.Id)
                .ToList();
            tree.Filters.ForAllGroups(grp => tree.FilterGroups.GetOrAdd(grp.FilterInfo.FilterToken, () => grp));
        }

        private static void ExtractAbsorbedFilters(Tree tree)
        {
            var freeFilters = new List<IFilter>();
            tree.Filters.ForGroups(grp => grp.ExtractAbsorbedFilters(freeFilters, tree));
            tree.Filters.AddRange(freeFilters);
        }

        private static void ReplaceGroupFiltersWithExists(Tree tree)
        {
            var freeFilters = new List<IFilter>();
            tree.Filters.ForAllGroups(grp => grp.ReplaceGroupFiltersWithExists(freeFilters, tree));
            tree.Filters.AddRange(freeFilters);
        }

        public static void PreprocessFilters(Tree tree)
        {
            ExtractAbsorbedFilters(tree);
            tree.Filters.AddRange(CreateExistsFiltersForGroups(tree, tree.Filters));
            ReplaceGroupFiltersWithExists(tree);
        }

        public static List<ExistsFilter> CreateExistsFiltersForGroups(Tree tree, IEnumerable<IFilter> filters)
        {
            return filters.OfType<IFilterGroup>()
                          .Select(tree.GetNode)
                          .Where(x => x != null)
                          .Select(x => x.CreateExistsFilter())
                          .ToList();
        }

        public static void ReorderNodesForFilters(Tree tree)
        {
            tree.Filters.ForEach(x => x.ForDependencies((dep1, dep2) => ReorderNodes(x, tree, dep1, dep2)));
            ExtractReorderedExistFiltersFromGroups(tree);
        }

        public static void ExtractReorderedExistFiltersFromGroups(Tree tree)
        {
            var freeFilters = new List<IFilter>();
            tree.Filters.ForAllGroups(grp => grp.ExtractReorderedExistFilters(freeFilters, tree));
            tree.Filters.AddRange(freeFilters);
        }

        public static void GetFinalFilters(Tree tree)
        {
            tree.Filters.ForAllGroups(grp => grp.Filters = GetFinalFilters(tree, grp.Filters));
            tree.Filters = GetFinalFilters(tree, tree.Filters);
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
                : tree.Store.FilterInversions.SafeGet(filter.FilterInfo.FilterToken);
            return inversionDiag != null ? new Inverter(filter, new FilterInfo(tree.Store.NextId, inversionDiag)) : filter;
        }
    }
}
