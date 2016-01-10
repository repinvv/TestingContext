namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;
    using static Subsystems.FilterProcessingService;

    internal static class TreeContextService
    {
        public static TreeContext CreateTreeContext(this TokenStore store, Tree tree)
        {
            var context = new TreeContext
            {
                Tree = tree,
                Store = store,
                Filters = GetTreeFilters(store)
            };

            context.Groups = context.Filters.OfType<IFilterGroup>().ToDictionary(x => x.FilterInfo.FilterToken);
            context.FiltersInGroup = context
                .Filters
                .GroupBy(x => x.ParentGroupToken)
                .ToDictionary(x => x.Key, x => x.ToList());
            context.ProviderTokensInGroup = store
                .Providers
                .GroupBy(x => x.Value.ParentGroupToken)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Key).ToList());
            return context;
        }
    }
}
