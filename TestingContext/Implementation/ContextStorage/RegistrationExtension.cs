namespace TestingContextCore.Implementation.ContextStorage
{
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Sources;

    internal static class RegistrationExtension
    {
        public static void RegisterFilter(this ContextStore store, IFilter filter)
        {
            foreach (var entityDefinition in filter.Definitions)
            {
                store.AllFilters.GetList(entityDefinition).Add(filter);
            }
        }

        public static void RegisterSource(this ContextStore store, ISource source)
        {
            if (store.Sources.ContainsKey(source.Definition))
            {
                throw new SourceRegistrationException($"Source for {source.Definition.Type.Name} with key {source.Definition.Key} already registered");
            }

            store.Sources.Add(source.Definition, source);
        }

        public static void RegisterDependency(this ContextStore store, Definition definition, ISource source)
        {
            store.Dependencies.GetList(definition).Add(source);
        }

        public static ISource GetSource(this ContextStore store, Definition definition)
        {
            ISource source;
            if (!store.Sources.TryGetValue(definition, out source))
            {
                throw new SourceRegistrationException($"Source for {definition} is not registered");
            }

            return source;
        }
    }
}
