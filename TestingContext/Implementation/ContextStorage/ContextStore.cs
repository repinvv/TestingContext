namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Sources;

    internal class ContextStore
    {
        public Dictionary<Definition, List<IFilter>> AllFilters { get; } = new Dictionary<Definition, List<IFilter>>();

        public Dictionary<Definition, List<IFilter>> ValidatedFilters { get; } = new Dictionary<Definition, List<IFilter>>();

        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();
        public Dictionary<Definition, List<INode>> Dependencies { get; } = new Dictionary<Definition, List<INode>>();
    }
}
