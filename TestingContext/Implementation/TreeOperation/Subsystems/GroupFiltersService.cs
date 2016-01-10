namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal static class GroupFiltersService
    {
        public static List<IFilter> GetInGroupFilters(this TreeContext context, IFilterGroup filterGroup)
        {
            return context.AllFiltersInGroup
                          .GetOrAdd(filterGroup.FilterInfo.FilterToken,
                                    () => context.CollectInGroupFilters(filterGroup));
        }

        private static List<IFilter> CollectInGroupFilters(this TreeContext context, IFilterGroup filterGroup)
        {
            var filters = context.FiltersInGroup
                                .SafeGet(filterGroup.FilterInfo.FilterToken, () => new List<IFilter>())
                                .ToList();
            filters.AddRange(filters.OfType<IFilterGroup>().SelectMany(context.GetInGroupFilters).ToList());
            return filters;
        }

        public static List<IToken> GetInGroupTokens(this TreeContext context, IFilterGroup filterGroup)
        {
            return context.AllProviderTokensInGroup
                          .GetOrAdd(filterGroup.FilterInfo.FilterToken,
                                    () => context.CollectInGroupTokens(filterGroup));
        }

        private static List<IToken> CollectInGroupTokens(this TreeContext context, IFilterGroup filterGroup)
        {
            var tokens = context.ProviderTokensInGroup
                                .SafeGet(filterGroup.FilterInfo.FilterToken, () => new List<IToken>())
                                .ToList();
            foreach (var @group in context.FiltersInGroup.SafeGet(filterGroup.FilterInfo.FilterToken, () => new List<IFilter>()).OfType<IFilterGroup>())
            {
                ((Action<IFilterGroup>)(grp => tokens.AddRange(context.GetInGroupTokens(grp))))(@group);
            }
            return tokens;
        }

        public static IEnumerable<IDependency> GetGroupDependencies(TreeContext context, IFilterGroup filterGroup)
        {
            var inGroupTokens = context.GetInGroupTokens(filterGroup);
            var groupDependencies = inGroupTokens
                .SelectMany(x => context.Store.Providers[x].Dependencies)
                .Concat(context.GetInGroupFilters(filterGroup).SelectMany(x => x.Dependencies))
                .Concat(filterGroup.Dependencies)
                .Where(dependency => !inGroupTokens.Contains(dependency.Token))
                .DistinctDependencies()
                .ToList();
            return groupDependencies;
        }

        public static IEnumerable<IDependency> DistinctDependencies(this IEnumerable<IDependency> input)
        {
            return input.GroupBy(dep => new { dep.Token, dep.Type })
                        .Select(x => x.First());
        }
    }
}
