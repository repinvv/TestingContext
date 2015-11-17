namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        Definition Definition { get; }

        INode SourceParent { get; set; }

        INode Parent { get; set; }

        NodeFilters Filters { get; }

        NodeResolver Resolver { get; }

        IProvider Provider { get; }

        bool IsChildOf(INode node);

        List<INode> GetParentalChain();

        List<INode> GetSourceChain();
    }
}
