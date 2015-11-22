namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextCore.UsefulExtensions;
    using static NodeReorderingService;
    using static NonEqualFilteringService;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(Tree tree, List<Node> nodes, TokenStore store)
        {
            var dict = GroupNodes(nodes);
            var nodesQueue = new Queue<INode>(new[] { tree.Root });
            var assigned = new HashSet<IToken> { tree.Root.Token };
            while (nodesQueue.Any())
            {
                var current = nodesQueue.Dequeue();
                List<Node> children;
                if (!dict.TryGetValue(current.Token, out children))
                {
                    continue;
                }

                foreach (var child in children.Where(child => child.Provider.Dependencies.All(x => assigned.Contains(x.Token))))
                {
                    ReorderNodes(tree, child.Provider);
                    AssignNonEqualFilters(tree, child.Provider, store);
                    var parent = FilterAssignmentService.GetAssignmentNode(tree, child.Provider);
                    child.Parent = parent;
                    child.SourceParent = parent;
                    assigned.Add(child.Token);
                    nodesQueue.Enqueue(child);
                }
            }

            foreach (var node in nodes.Where(node => !assigned.Contains(node.Token)))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.");
            }
        }

        private static Dictionary<IToken, List<Node>> GroupNodes(List<Node> nodes)
        {
            var dict = new Dictionary<IToken, List<Node>>();
            foreach (var node in nodes)
            {
                foreach (var dependency in node.Provider.Dependencies)
                {
                    dict.GetList(dependency.Token).Add(node);
                }
            }
            
            return dict;
        }
    }
}
