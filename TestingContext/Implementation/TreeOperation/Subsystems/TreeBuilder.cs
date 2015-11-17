namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Interfaces;
    using static NodeReorderingService;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(Tree tree, List<Node> nodes)
        {
            var dict = GroupNodes(nodes);
            var nodesQueue = new Queue<INode>(new[] { tree.Root });
            var assigned = new HashSet<Definition> { tree.Root.Definition };
            while (nodesQueue.Any())
            {
                var current = nodesQueue.Dequeue();
                List<Node> children;
                if (!dict.TryGetValue(current.Definition, out children))
                {
                    continue;
                }

                foreach (var child in children.Where(child => child.Provider.Dependencies.All(x => assigned.Contains(x.Definition))))
                {
                    ReorderNodes(tree, child.Provider);
                    var parent = FilterAssignmentService.GetAssignmentNode(tree, child.Provider);
                    child.Parent = parent;
                    child.SourceParent = parent;
                    assigned.Add(child.Definition);
                    nodesQueue.Enqueue(child);
                }
            }

            foreach (var node in nodes.Where(node => !assigned.Contains(node.Definition)))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.");
            }
        }

        private static Dictionary<Definition, List<Node>> GroupNodes(List<Node> nodes)
        {
            var dict = new Dictionary<Definition, List<Node>>();
            foreach (var node in nodes)
            {
                foreach (var dependency in node.Provider.Dependencies)
                {
                    dict.GetList(dependency.Definition).Add(node);
                }
            }
            
            return dict;
        }
    }
}
