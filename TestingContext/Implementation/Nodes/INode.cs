namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.Providers;

    internal interface INode
    {
        IProvider Provider { get; }

        INode Root { get; }

        bool IsChildOf(INode node);
    }
}
