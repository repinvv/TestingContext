namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;

    internal class RootNode : INode
    {
        private readonly AndGroup andGroup = new AndGroup();

        public RootNode(Definition definition)
        {
            Definition = definition;
        }

        public Definition Definition { get; }

        public List<INode> Children { get; } = new List<INode>();

        public INode Parent { get; set; }
        
        public IProvider Provider => null;

        public bool IsChildOf(INode node) => false;

        public IFilter ItemFilter => andGroup;

        public IFilter CollectionFilter => null;

        public List<INode> GetNodesChain() => new List<INode>() { this };

        public void AddItemFilter(IFilter filter) => andGroup.AddFilter(filter);

        public void AddCollectionFilter(IFilter filter) { }
    }
}
