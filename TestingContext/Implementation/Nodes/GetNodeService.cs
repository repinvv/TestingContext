namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Tokens;

    internal static class GetNodeService
    {
        public static INode GetNode(this Tree tree, IToken token)
        {
            INode node;
            if (!tree.Nodes.TryGetValue(token, out node))
            {
                throw new AlgorythmException($"Node for {token} is not created.");
            }

            return node;
        }
    }
}
