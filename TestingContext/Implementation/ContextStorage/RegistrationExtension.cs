namespace TestingContextCore.Implementation.ContextStorage
{
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Sources;

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
            if (store.Sources.ContainsKey(source.EntityDefinition))
            {
                throw new SourceRegistrationException($"Source for {source.EntityDefinition.Type.Name} with key {source.EntityDefinition.Key} already registered");
            }

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
