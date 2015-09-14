namespace TestingContextCore.Implementation.ContextStorage
{
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal static class RegistrationExtension
    {
        public static void RegisterFilter(this ContextStore store, Definition definition, IFilter filter)
        {
            store.CheckResolutionStarted();
            store.Filters.GetList(definition).Add(filter);
        }

        public static void RegisterNode(this ContextStore store, Definition definition, INode node)
        {
            store.CheckResolutionStarted();
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

        private static void CheckResolutionStarted(this ContextStore store)
        {
            if (store.ResolutionStarted)
            {
                throw new ResolutionStartedException("Resolutions are already started, can't add more registrations");
            }
        }
    }
}
