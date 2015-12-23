namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
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
            filterGroup.Filters.ForAllGroups(grp => tokens.AddRange(GetCvTokens(grp, tree)));
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
    }
}
