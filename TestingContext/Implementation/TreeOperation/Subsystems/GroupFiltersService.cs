namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal static class GroupFiltersService
    { 
        private static IEnumerable<IToken> GetCvTokens(IFilterGroup group, Tree tree)
        {
            return group.Filters
                .Where(tree.IsCvFilter)
                .Select(filter => filter.Dependencies.First().Token);
        }

        public static List<IToken> GetInGroupTokens(IFilterGroup filterGroup, Tree tree)
        {
            var tokens = GetCvTokens(filterGroup, tree).ToList();
            filterGroup.Filters.ForGroups(grp => tokens.AddRange(GetCvTokens(grp, tree)));
            return tokens;
        }

        public static HashSet<IDependency> GetGroupDependencies(IFilterGroup group, HashSet<IToken> inGroupTokens, Tree tree)
        {
            var dependencies = new HashSet<IDependency>(group.GroupDependencies);
            foreach (var dependency in inGroupTokens
                .SelectMany(x => tree.Store.Providers[x].Dependencies)
                .Concat(group.Dependencies)
                .Where(dependency => !inGroupTokens.Contains(dependency.Token)))
            {
                dependencies.Add(dependency);
            }

            return dependencies;
        }

        public static void ReplaceGroupFiltersWithExists(this IFilterGroup group, List<IFilter> freeFilters, Tree tree)
        {
            var groupsWithNodes = group.Filters.OfType<IFilterGroup>()
                                       .Where(grp => tree.GetNode(grp) != null)
                                       .ToList();
            freeFilters.AddRange(groupsWithNodes);
            var existFilters = groupsWithNodes.Select(x => tree.GetNode(x).CreateExistsFilter()).ToList();
            group.Filters = group.Filters.Except(groupsWithNodes)
                                 .Concat(existFilters)
                                 .ToList();
        }

        public static void ExtractReorderedExistFilters(this IFilterGroup group, List<IFilter> freeFilters, Tree tree)
        {
            var reorderedExistsFilters = group.Filters.OfType<ExistsFilter>()
                                              .Where(x => IsNodeOfExistsFilterReordered(x, group, tree))
                                              .ToList();
            freeFilters.AddRange(reorderedExistsFilters);
            group.Filters = group.Filters.Except(reorderedExistsFilters)
                                 .ToList();
        }

        private static bool IsNodeOfExistsFilterReordered(IFilter existsFilter, IFilterGroup group, Tree tree)
        {
            var groupNode = tree.GetNode(group);
            var existFilterNode = tree.GetNode(existsFilter.Dependencies.First().Token);
            return existFilterNode.Parent != groupNode;
        }
    }
}
