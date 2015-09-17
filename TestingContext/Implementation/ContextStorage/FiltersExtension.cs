namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;

    internal static class FiltersExtension
    {
        public static List<IFilter> GetFilters(this ContextStore store, Definition definition)
        {
            return store.Filters.SafeGet(definition, () => new List<IFilter>());
        }
    }
}
