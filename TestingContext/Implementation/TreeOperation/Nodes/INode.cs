namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        Definition Definition { get; }

        List<INode> Children { get; }

        INode Parent { get; set; }

        NodeFilters Filters { get; }

        NodeResolver Resolver { get; }

        IProvider Provider { get; }

        bool IsChildOf(INode node);

        List<INode> GetNodesChain();


    }
}
