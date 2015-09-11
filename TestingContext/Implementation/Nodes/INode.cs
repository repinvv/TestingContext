namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.Sources;

    internal interface INode
    {
        ISource Source { get; }

        INode Root { get; }

        bool IsChildOf(INode node);
    }
}
