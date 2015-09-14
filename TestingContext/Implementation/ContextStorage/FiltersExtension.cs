namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;

    internal static class FiltersExtension
    {
        public static List<IFilter> GetFilters(this ContextStore store, Definition definition)
        {
            var filters = store.Filters
                               .SafeGet(definition, () => new List<IFilter>())
                               .ToList();

            filters.ForEach(x => ValidateFilter(store, x));
            return filters;
        }

        private static void ValidateFilter(ContextStore store, IFilter filter)
        {
            var firstNode = store.GetNode(filter.Definitions[0]);
            for (int n = 1; n < filter.Definitions.Length; n++)
            {
                var nextNode = store.GetNode(filter.Definitions[n]);
                if (!firstNode.IsChildOf(nextNode))
                {
                    throw new FilterRegistrationException($"Filter, registered for {filter.Definitions[0]} can't work with {filter.Definitions[n]}, With<> can only be" +
                                                          $" used for definitions in the same registration branc, i.e. this definition can only be a parent of {filter.Definitions[0]}.");   
                }
            }
        }
    }
}
