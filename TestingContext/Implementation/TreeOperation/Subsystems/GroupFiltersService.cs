namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;

    internal static class GroupFiltersService
    { 
        private static IEnumerable<IToken> GetCvTokens(IFilterGroup group, TokenStore store)
        {
            return group.Filters
                .Where(store.IsCvFilter)
                .Select(filter => filter.Dependencies.First().Token);
        }

        public static List<IToken> GetInGroupTokens(IFilterGroup filterGroup, TokenStore store)
        {
            var tokens = GetCvTokens(filterGroup, store).ToList();
            filterGroup.Filters.ForGroups(grp =>tokens.AddRange(GetCvTokens(grp, store)));
            return tokens;
        }

        public static HashSet<IDependency> GetGroupDependencies(IFilterGroup group, HashSet<IToken> inGroupTokens, TokenStore store)
        {
            var dependencies = new HashSet<IDependency>(group.GroupDependencies);
            foreach (var dependency in inGroupTokens
                .SelectMany(x => store.Providers[x].Dependencies)
                .Concat(group.Dependencies)
                .Where(dependency => !inGroupTokens.Contains(dependency.Token)))
            {
                dependencies.Add(dependency);
            }

            return dependencies;
        }
    }
}
