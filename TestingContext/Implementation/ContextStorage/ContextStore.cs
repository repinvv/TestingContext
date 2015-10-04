namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Interfaces;

    internal class ContextStore
    {
        public ContextStore(Definition rootDefinition)
        {
            RootDefinition = rootDefinition;
            RootNode = new RootNode(null, this);
            this.RegisterNode(RootDefinition, RootNode);
        }

        public IResolutionLog Log { get; set; }
        public Definition LastRegistered { get; set; }
        public Definition RootDefinition { get; }
        public INode RootNode { get; }
        public bool ResolutionStarted { get; set; }

        public Dictionary<Definition, List<IFilter>> Filters { get; } = new Dictionary<Definition, List<IFilter>>();
        public Dictionary<string, List<IFilter>> KeyedFilters { get; } = new Dictionary<string, List<IFilter>>();
        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();
        public Dictionary<Definition, List<INode>> Dependendents { get; } = new Dictionary<Definition, List<INode>>();
        public List<IDependency> Dependencies { get; } = new List<IDependency>();
        public HashSet<Swap> Swaps { get; } = new HashSet<Swap>();
    }
}
