namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;

    internal static class TreeContextService
    {
        public static TreeContext CreateTreeContext(this TokenStore store, Tree tree)
        {
            var context = new TreeContext
            {
                Tree = tree,
                Store = store,
                Filters = store.GetTreeFilters()
            };

            context.Groups = context.Filters.OfType<IFilterGroup>().ToDictionary(x => x.FilterInfo.FilterToken);
            context.FiltersInGroup = context
                .Filters
                .Where(x => x.ParentGroupToken != null)
                .GroupBy(x => x.ParentGroupToken)
                .ToDictionary(x => x.Key, x => x.ToList());
            context.ProviderTokensInGroup = store
                .Providers
                .Where(x => x.Value.ParentGroupToken != null)
                .GroupBy(x => x.Value.ParentGroupToken)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Key).ToList());
            return context;
        }
    }
}
