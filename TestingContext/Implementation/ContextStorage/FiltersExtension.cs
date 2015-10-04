namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;

    internal static class FiltersExtension
    {
        public static void RegisterFilter(this ContextStore store, Definition definition, IFilter filter, string id)
        {
            store.CheckResolutionStarted();
            store.Filters.GetList(definition).Add(filter);
            if (id != null)
            {
                store.KeyedFilters.GetList(id).Add(filter);
            }
        }

        public static void InvertFilter(this ContextStore store, string id)
        {
            store.CheckResolutionStarted();
            List<IFilter> list;
            if (id == null || !store.KeyedFilters.TryGetValue(id, out list))
            {
                throw new RegistrationException($"Filter {id} is not registered");
            }

            foreach (var filter in store.KeyedFilters[id])
            {
                filter.Invert();
            }
        }
    }
}
