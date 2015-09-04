namespace TestingContextCore.Implementation.ContextStore
{
    using System.Collections.Generic;
    using System.Linq;
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

            return filtersList.Where(x => x.EntityDefinitions.Where(y => !Equals(y, definition)).All(y => !store.IsChildOf(y, definition)));
        }

        private static bool IsChildOf(this ContextStore store, EntityDefinition filterDefinition, EntityDefinition sourceDefinition)
        {
            var source = store.Sources[filterDefinition].Parent;

            while (source != null)
            {
                if (Equals(source.EntityDefinition, sourceDefinition))
                {
                    return true;
                }

                source = source.Parent;
            }

            return false;
        }
    }
}
