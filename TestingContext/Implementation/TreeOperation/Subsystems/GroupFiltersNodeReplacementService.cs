namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal static class GroupFiltersNodeReplacementService
    {
        public static void ReplaceGroupFiltersWithExists(this IFilterGroup group, List<IFilter> freeFilters, Tree tree)
        {
            var groupsWithNodes = group.Filters.OfType<IFilterGroup>()
                                       .Where(grp => !grp.IsSameGroup(group))
                                       .Where(grp => tree.GetNode(grp) != null).ToList();
            freeFilters.AddRange(groupsWithNodes);
            var existFilters = groupsWithNodes.Select(x => tree.GetNode(x).CreateExistsFilter()).ToList();
            group.Filters = group.Filters
                                 .Except(groupsWithNodes)
                                 .Concat(existFilters)
                                 .ToList();
        }
    }
}
