namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    internal interface INode
    {
        IToken Token { get; }

        INode SourceParent { get; set; }

        INode Parent { get; set; }

        //NodeFilters Filters { get; }

        //NodeResolver Resolver { get; }

        //IProvider Provider { get; }

        bool IsChildOf(INode node);

        bool IsSourceChildOf(INode node);

        List<INode> GetParentalChain();

        List<INode> GetSourceChain();
    }
}
