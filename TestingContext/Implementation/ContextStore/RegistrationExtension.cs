namespace TestingContextCore.Implementation.ContextStore
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;

    internal static class RegistrationExtension
    {
        public static void RegisterFilter(this ContextStore store, IFilter filter)
        {
            foreach (var entityDefinition in filter.EntityDefinitions)
            {
                store.Filters.GetList(entityDefinition).Add(filter);
            }
        }

        public static void RegisterSource(this ContextStore store, ISource source)
        {
            store.Sources.Add(source.EntityDefinition, source);
        }

        public static void RegisterDependency(this ContextStore store, EntityDefinition definition, ISource source)
        {
            store.Dependencies.GetList(definition).Add(source);
        }

        public static ISource GetSource(this ContextStore store, EntityDefinition definition)
        {
            return store.Sources[definition];
        }
    }
}
