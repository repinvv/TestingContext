namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextCore.UsefulExtensions;
    using static NodeReorderingService;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(TokenStore store, List<Node> nodes)
        {
            var dict = GroupNodes(nodes);
            var nodesQueue = new Queue<INode>(new[] { store.Tree.Root });
            var assigned = new HashSet<IToken> { store.Tree.Root.Token };
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
                    ReorderNodes(store, child.Provider.Dependencies.ToArray(), child.Provider.CollectionValidityFilter);
                    var parent = FilterAssignmentService.GetAssignmentNode(store.Tree, child.Provider);
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
