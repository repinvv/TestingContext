namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Linq;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Nodes;

    internal static class ValidationExtension
    {
        public static void ValidateDependencies(this ContextStore store)
        {
            foreach (var dependency in store.Dependencies)
            {
                dependency.Validate(store);
            }
        }

        /// <summary>
        /// Validates dependency by trying to swap branches, 
        /// if branches were already swapped the other way, it means that circular reference exists
        /// Returns closest parent of 2 nodes, for performance reasons
        /// </summary>
        /// <param name="store"></param>
        /// <param name="node"></param>
        /// <param name="dependNode"></param>
        /// <returns>closest parent</returns>
        public static Definition ValidateDependency(this ContextStore store, INode node, INode dependNode)
        {
            if (dependNode.IsChildOf(node))
            {
                throw new ResolutionException($"{node} is registered use {dependNode} as singular dependency, " +
                                              $"while {dependNode} is registered as a child of {node}. " +
                                              "This is a prohibited scenario, singular dependency can only reference parent, " +
                                              "or other cranches.");
            }
            var nodeList = node.DefinitionChain;
            var dependList = dependNode.DefinitionChain;

            Definition parent = store.RootDefinition;
            int i = 0;
            while (nodeList[i] == dependList[i])
            {
                parent = nodeList[i++];
            }

            var swap = new Swap(parent, nodeList[i], dependList[i]);
            if (store.Swaps.Contains(swap.Backward))
            {
                throw new ResolutionException($"{node.DefinitionChain.Last()} is registered to depend on " +
                                              $"{dependNode.DefinitionChain.Last()}, which makes circular reference.");
            }

            store.Swaps.Add(swap);
            store.Swap(swap);

            return parent;
        }

        public static void Swap(this ContextStore store, Swap swap)
        {
            if (swap.Parent == store.RootDefinition)
            {
                return;
            }

            var node = store.GetNode(swap.Child);
            var dependNode = store.GetNode(swap.DependedChild);
            var list = store.Dependendents[swap.Parent];

            var nodeIndex = list.IndexOf(node);
            var dependIndex = list.IndexOf(dependNode);
            if (nodeIndex > dependIndex)
            {
                return; // no swap needed, dependency is already in front of node
            }

            list[nodeIndex] = dependNode;
            list[dependIndex] = node;
        }
    }
}
