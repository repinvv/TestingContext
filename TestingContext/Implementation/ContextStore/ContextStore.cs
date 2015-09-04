namespace TestingContextCore.Implementation.ContextStore
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;

    internal class ContextStore
    {
        public readonly Dictionary<EntityDefinition, List<IFilter>> Filters = new Dictionary<EntityDefinition, List<IFilter>>();
        public readonly Dictionary<EntityDefinition, ISource> Sources = new Dictionary<EntityDefinition, ISource>();
        public readonly Dictionary<EntityDefinition, List<ISource>> Dependencies = new Dictionary<EntityDefinition, List<ISource>>();
    }
}
