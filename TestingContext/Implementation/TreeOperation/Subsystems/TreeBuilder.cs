namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.PublicMembers.Exceptions;
    using static FilterAssignmentService;
    using static NodeReorderingService;

    internal static class TreeBuilder
    {
        public static void BuildNodesTree(Tree tree, Dictionary<IToken, List<INode>> nodeDependencies)
        {
            var nodesQueue = new Queue<INode>(new[] { tree.Root });
            var assigned = new HashSet<IToken> { tree.Root.Token };
            while (nodesQueue.Any())
            {
                var current = nodesQueue.Dequeue();
                List<INode> children;
                if (!nodeDependencies.TryGetValue(current.Token, out children))
                {
                    continue;
                }

                foreach (var child in children.Where(child => child.Provider.Dependencies.All(x => assigned.Contains(x.Token))))
                {
                    if (child.Provider.Group != null && !assigned.Contains(child.Provider.Group.GroupToken))
                    {
                        continue;
                    }

                    ReorderNodes(tree, child.Provider);
                    var parent = GetAssignmentNode(tree, child.Provider);
                    child.Parent = parent;
                    child.SourceParent = parent;
                    assigned.Add(child.Token);
                    nodesQueue.Enqueue(child);
                }
            }

            foreach (var node in nodeDependencies.SelectMany(x=>x.Value).Distinct().Where(node => !assigned.Contains(node.Token)))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.", node.Provider.DiagInfo);
            }
        }
    }
}
