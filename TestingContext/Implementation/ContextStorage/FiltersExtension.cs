namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;

    internal static class FiltersExtension
    {
        public static IList<IFilter> GetFilters(this ContextStore store, Definition definition)
        {
            return store.ValidatedFilters.SafeGet(definition, () => GetValidatedFilters(store, definition));
        }

        private static List<IFilter> GetValidatedFilters(ContextStore store, Definition definition)
        {
            var definitionSource = store.GetSource(definition);

            var filters = store.AllFilters
                               .SafeGet(definition, () => new List<IFilter>())
                               .Where(x => x.Definitions.All(y => !store.GetSource(y).IsChildOf(definitionSource)))
                               .ToList();
            filters.ForEach(x=>ValidateFilter(store, x));
            store.ValidatedFilters[definition] = filters;
            return filters;
        }

        private static void ValidateFilter(ContextStore store, IFilter filter)
        {
            for (int i = 0; i < filter.Definitions.Length - 1; i++)
            {
                for (int j = 1; j < filter.Definitions.Length; j++)
                {
                    ValidateDefinitions(store, filter.Definitions[i], filter.Definitions[j]);
                }
            }
        }

        private static void ValidateDefinitions(ContextStore store, Definition definition, Definition definition1)
        {
            var source = store.GetSource(definition);
            var source1 = store.GetSource(definition1);
            if (source.Root == source1.Root)
            {
                if (!source.IsChildOf(source1) && !source1.IsChildOf(source))
                {
                    throw new FilterRegistrationException($"filter, includes {definition} and {definition1} which have the same root, but in different branches. " +
                                                          $"Definitions in the filter should belong to the same branch or be in different independent hierarchies.");
                }
            }
        }
    }
}
