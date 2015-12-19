namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.TreeOperation;

    internal interface INode
    {
        int Id { get; }

        Tree Tree { get; }

        IToken Token { get; }

        INode SourceParent { get; set; }

        INode Parent { get; set; }

        List<INode> Children { get; }

        NodeFilterInfo FilterInfo { get; }

        NodeResolver Resolver { get; }

        IProvider Provider { get; }

        bool IsNegative { get; }

        bool IsChildOf(INode node);

        List<INode> GetParentalChain();

        List<INode> GetSourceChain();
    }
}
