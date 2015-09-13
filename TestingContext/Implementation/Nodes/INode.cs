namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        IProvider Provider { get; }

        INode Root { get; }

        bool IsChildOf(INode node);

        List<Definition> DefinitionChain { get; }
    }
}
