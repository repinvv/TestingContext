namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.PublicMembers.Exceptions;
    using static NodeReorderingService;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(Tree tree, List<Node> nodes)
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
                    ReorderNodes(tree, child.Provider.Dependencies.ToArray(), child.Provider.DiagInfo);
                    var parent = FilterAssignmentService.GetAssignmentNode(tree, child.Provider);
                    child.Parent = parent;
                    child.SourceParent = parent;
                    assigned.Add(child.Token);
                    nodesQueue.Enqueue(child);
                }
            }

            foreach (var node in nodes.Where(node => !assigned.Contains(node.Token)))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.", node.Provider.CollectionValidityFilter.DiagInfo);
            }
        }

        private static Dictionary<IToken, List<Node>> GroupNodes(List<Node> nodes)
        {
            var dict = new Dictionary<IToken, List<Node>>();
            foreach (var node in nodes)
            {
                DependencyCheck.CheckDependencies(node.Provider, node.Token);
                foreach (var dependency in node.Provider.Dependencies)
                {
                    dict.GetList(dependency.Token).Add(node);
                }
            }
            
            return dict;
        }
    }
}
