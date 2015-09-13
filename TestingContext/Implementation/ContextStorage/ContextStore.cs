namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal class ContextStore
    {
        public bool ResolutionStarted { get; set; }
        public Dictionary<Definition, List<IFilter>> Filters { get; } = new Dictionary<Definition, List<IFilter>>();
        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();
        public Dictionary<Definition, List<INode>> Dependencies { get; } = new Dictionary<Definition, List<INode>>();
    }
}
