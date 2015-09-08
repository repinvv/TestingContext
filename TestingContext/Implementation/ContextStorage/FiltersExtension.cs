namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;

    internal static class FiltersExtension
    {
        public static IEnumerable<IFilter> GetFilters(this ContextStore store, EntityDefinition definition)
        {
            List<IFilter> filtersList;
            if (!store.Filters.TryGetValue(definition, out filtersList))
            {
                return new List<IFilter>();
            }

            return filtersList;
        }
    }
}
