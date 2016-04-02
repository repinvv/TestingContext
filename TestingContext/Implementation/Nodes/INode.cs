namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextLimitedInterface.Tokens;

    internal interface INode
    {
        int Weight { get; set; }

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
