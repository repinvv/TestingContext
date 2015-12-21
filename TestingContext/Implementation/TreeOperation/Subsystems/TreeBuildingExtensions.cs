namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class TreeBuildingExtensions
    {
        public static bool IsCvFilter(this Tree tree, IFilter filter)
        {
            return tree.Store.CvFilters.Contains(filter.FilterInfo.Token);
        }

        // can be used after the tree is built
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Token);
            return dependency.Type == DependencyType.CollectionValidity
                       ? node.Parent
                       : dependency.Type == DependencyType.Collection
                             ? node.SourceParent
                             : node;
        }

        public static void ForGroups(this List<IFilter> filters, Action<IFilterGroup> action)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                action(group);
                ForGroups(group.Filters, action);
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

        public static HashSet<IToken> ForNodes(this Dictionary<IToken, List<INode>> nodeDependencies, Tree tree, Action<INode> action)
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

                foreach (var child in children)
                {
                    var parentGroup = tree.GetParentGroup(child.Provider);
                    if (!child.Provider.Dependencies.All(x => assigned.Contains(x.Token)) 
                        || (parentGroup != null && !assigned.Contains(parentGroup?.NodeToken)))
                    {
                        continue;
                    }

                    action(child);
                    assigned.Add(child.Token);
                    nodesQueue.Enqueue(child);
                }
            }

            return assigned;
        }

        public static IFilterGroup GetParentGroup(this Tree tree, IDepend depend)
        {
            return depend.GroupToken == null
                ? null :
                tree.FilterGroups[depend.GroupToken];
        }

        public static bool IsParent(this Tree tree, IToken token1, IToken token2)
        {
            return tree.GetParents(token1).Contains(token2);
        }

        public static HashSet<IToken> GetParents(this Tree tree, IToken token)
        {
            return tree.Parents.GetOrAdd(token, () => FindParents(tree, token));
        }

        private static HashSet<IToken> FindParents(Tree tree, IToken token)
        {
            if (token == tree.Store.RootToken)
            {
                return new HashSet<IToken>();
            }

            var dependencies = tree.Store.Providers[token].Dependencies.Select(x => x.Token).ToList();
            return new HashSet<IToken>(dependencies.SelectMany(tree.GetParents).Concat(dependencies));
        }
    }
}
