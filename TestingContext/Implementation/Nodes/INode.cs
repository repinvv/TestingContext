namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        IToken Token { get; }

        INode SourceParent { get; set; }

        INode Parent { get; set; }

        NodeFilterInfo FilterInfo { get; }

        NodeResolver Resolver { get; }

        IProvider Provider { get; }

        bool IsChildOf(INode node);

        List<INode> GetParentalChain();

        List<INode> GetSourceChain();
    }
}
