namespace TestingContextCore.Implementation.ContextStorage
{
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal static class RegistrationExtension
    {
        public static void RegisterFilter(this ContextStore store, IFilter filter)
        {
            foreach (var entityDefinition in filter.Definitions)
            {
                store.Filters.GetList(entityDefinition).Add(filter);
            }
        }

        public static void RegisterNode(this ContextStore store, Definition definition, INode node)
        {
            if (store.Nodes.ContainsKey(definition))
            {
                throw new SourceRegistrationException($"Source for {definition} already registered");
            }

            store.Nodes.Add(definition, node);
        }

        public static void RegisterDependency(this ContextStore store, Definition definition, INode node)
        {
            store.Dependencies.GetList(definition).Add(node);
        }

        public static INode GetNode(this ContextStore store, Definition definition)
        {
            INode node;
            if (!store.Nodes.TryGetValue(definition, out node))
            {
                throw new SourceRegistrationException($"Source for {definition} is not registered");
            }

            return node;
        }
    }
}
