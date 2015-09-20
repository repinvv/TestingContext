namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using static Implementation.Definition;

    internal class ContextStore
    {
        public ContextStore()
        {
            RootDefinition = Define<TestingContext>(string.Empty);
            this.RegisterNode(RootDefinition, new RootNode(null, RootDefinition));
        }
        
        public Definition RootDefinition { get; }
        public bool Logging { get; set; }
        public bool ResolutionStarted { get; set; }
        public Dictionary<Definition, List<IFilter>> Filters { get; } = new Dictionary<Definition, List<IFilter>>();
        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();
        public Dictionary<Definition, List<INode>> Dependendents { get; } = new Dictionary<Definition, List<INode>>();
        public List<IDependency> Dependencies { get; } = new List<IDependency>();
        public HashSet<Swap> Swaps { get; } = new HashSet<Swap>();
    }
}
