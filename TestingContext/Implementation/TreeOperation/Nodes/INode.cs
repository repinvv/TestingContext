namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        Definition Definition { get; }

        List<INode> Children { get; }

        INode Parent { get; set; }

        IFilter ItemFilter { get; }

        IFilter CollectionFilter { get; }

        IProvider Provider { get; }

        bool IsChildOf(INode node);

        List<INode> GetNodesChain();

        void AddItemFilter(IFilter filter);

        void AddCollectionFilter(IFilter filter);
    }
}
