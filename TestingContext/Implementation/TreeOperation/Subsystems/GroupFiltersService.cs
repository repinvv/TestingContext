namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;

    internal static class GroupFiltersService
    {
        public static List<IToken> GetInGroupTokens(IFilterGroup filterGroup, TokenStore store)
        {
            var tokens = new List<IToken>();
            foreach (var filter in filterGroup.Filters)
            {
                if (store.IsCvFilter(filter))
                {
                    tokens.Add(filter.Dependencies.First().Token);
                }
                else
                {
                    var group = filter as IFilterGroup;
                    if (group != null)
                    {
                        tokens.AddRange(GetInGroupTokens(group, store));
                    }
                }
            }

            return tokens;
        }
    }
}
