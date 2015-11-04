namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(RootNode root, List<Node> nodes)
        {
            var dict = nodes.GroupBy(x => x.Provider.Dependency.Definition).ToDictionary(x => x.Key);
            var nodesQueue = new Queue<INode>(new[] { root });
            while (nodesQueue.Any())
            {
                var current = nodesQueue.Dequeue();
                IGrouping<Definition, Node> children;
                if (dict.TryGetValue(current.Definition, out children))
                {
                    foreach (var child in children)
                    {
                        child.Parent = current;
                        child.SourceParent = current;
                        nodesQueue.Enqueue(child);
                    }
                }
            }
        }
    }
}
