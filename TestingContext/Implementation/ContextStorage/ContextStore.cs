namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Sources;

    internal class ContextStore
    {
        public Dictionary<Definition, List<IFilter>> AllFilters { get; } = new Dictionary<Definition, List<IFilter>>();
        public Dictionary<Definition, List<IFilter>> ValidatedFilters { get; } = new Dictionary<Definition, List<IFilter>>();
        public Dictionary<Definition, ISource> Sources { get; } = new Dictionary<Definition, ISource>();
        public Dictionary<Definition, List<ISource>> Dependencies { get; } = new Dictionary<Definition, List<ISource>>();
    }
}
