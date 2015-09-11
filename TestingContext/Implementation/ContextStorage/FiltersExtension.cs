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
            var definitionNode = store.GetNode(definition);

            var filters = store.AllFilters
                               .SafeGet(definition, () => new List<IFilter>())
                               .Where(x => x.Definitions.All(y => !store.GetNode(y).IsChildOf(definitionNode)))
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
            var node = store.GetNode(definition);
            var node1 = store.GetNode(definition1);
            if (node.Root == node1.Root)
            {
                if (!node.IsChildOf(node1) && !node1.IsChildOf(node))
                {
                    throw new FilterRegistrationException($"filter, includes {definition} and {definition1} which have the same root, but in different branches. " +
                                                          $"Definitions in the filter should belong to the same branch or be in different independent hierarchies.");
                }
            }
        }
    }
}
