namespace TestingContextCore.OldImplementation.TreeOperation
{
    using TestingContextCore.OldImplementation.Nodes;

    internal static class GetNodeExtension
    {
        public static INode GetNode(this Tree tree, Definition definition)
        {
            INode node;
            if (!tree.Nodes.TryGetValue(definition, out node))
            {
                throw new RegistrationException($"Entity {definition} is not registered.");
            }

            return node;
        }
    }
}
