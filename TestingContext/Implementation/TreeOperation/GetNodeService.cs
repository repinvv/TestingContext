namespace TestingContextCore.Implementation.TreeOperation
{
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class GetNodeService
    {
        public static INode GetNode(this Tree tree, IToken token)
        {
            INode node;
            if (!tree.Nodes.TryGetValue(token, out node))
            {
                throw new RegistrationException($"Entity {token} is not registered.");
            }

            return node;
        }
    }
}
