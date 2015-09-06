namespace TestingContextCore.Implementation.ContextStore
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;

    internal class ContextStore
    {
        public Dictionary<EntityDefinition, List<IFilter>> Filters { get; } = new Dictionary<EntityDefinition, List<IFilter>>();
        public Dictionary<EntityDefinition, ISource> Sources { get; } = new Dictionary<EntityDefinition, ISource>();
        public Dictionary<EntityDefinition, List<ISource>> Dependencies { get; } = new Dictionary<EntityDefinition, List<ISource>>();
    }
}
