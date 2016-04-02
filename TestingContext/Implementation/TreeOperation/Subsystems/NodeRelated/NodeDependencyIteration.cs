namespace TestingContextCore.Implementation.TreeOperation.Subsystems.NodeRelated
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextLimitedInterface.Tokens;

    internal static class NodeDependencyIteration
    {
        public static void ForNodes(this Dictionary<IToken, List<INode>> nodeDependencies, TreeContext context, Action<INode> action)
        {
            var nodesQueue = new Queue<INode>(new[] { context.Tree.Root });
            var assigned = new HashSet<IToken> { context.Tree.Root.Token };
            while (nodesQueue.Any())
            {
                var current = nodesQueue.Dequeue();
                List<INode> children;
                if (!nodeDependencies.TryGetValue(current.Token, out children))
                {
                    continue;
                }

                foreach (var child in children)
                {
                    var dependencies = context.GetDependencies(child);
                    if (!dependencies.All(x => assigned.Contains(x.Token)))
                    {
                        continue;
                    }

                    action(child);
                    assigned.Add(child.Token);
                    nodesQueue.Enqueue(child);
                }
            }
        }

        public static void ForDependencies(this IDepend depend, Action<IDependency, IDependency> action)
        {
            var dependencies = depend.Dependencies.ToArray();
            for (int i = 0; i < dependencies.Length; i++)
            {
                for (int j = i + 1; j < dependencies.Length; j++)
                {
                    if (dependencies[i].Token != dependencies[j].Token)
                    {
                        action(dependencies[i], dependencies[j]);
                    }
                }
            }
        }
    }
}
