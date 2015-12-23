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
    using TestingContextCore.PublicMembers;

    internal static class TreeBuildingExtensions
    {
        public static bool GroupIsSameAsParent(this Tree tree, IFilterGroup group)
        {
            return group.IsSameGroup(tree.GetParentGroup(group));
        }

        public static bool IsSameGroup(this IFilterGroup group, IFilterGroup parent)
        {
            return group.NodeToken == parent?.NodeToken;
        }

        public static IEnumerable<ExistsFilter> CreateExistsFiltersForGroups(this Tree tree, IEnumerable<IFilter> filters)
        {
            return filters.OfType<IFilterGroup>()
                          .Select(tree.GetNode)
                          .Where(x => x != null)
                          .Select(x => x.CreateExistsFilter());
        }

        public static ExistsFilter CreateExistsFilter(this INode node)
        {
            var info = new FilterInfo(node.Tree.Store.NextId);
            var dependency = new CollectionDependency(node.Token);
            return new ExistsFilter(dependency, info);
        }

        public static INode GetCvFilterNode(this Tree tree, IFilter filter)
        {
            return tree.GetNode(filter.Dependencies.First().Token);
        }

        public static INode GetNode(this Tree tree, IToken token)
        {
            return tree.Nodes.SafeGet(token);
        }

        public static INode GetNode(this Tree tree, IFilterGroup group)
        {
            return tree.GetNode(group.NodeToken);
        }

        public static bool IsCvFilter(this Tree tree, IFilter filter)
        {
            return tree.Store.CvFilters.Contains(filter.FilterInfo.Token);
        }

        // can be used after the tree is built
        public static INode GetDependencyNode(this Tree tree, IDependency dependency)
        {
            var node = tree.GetNode(dependency.Token);
            return dependency.Type == DependencyType.CollectionValidity
                       ? node.Parent
                       : dependency.Type == DependencyType.Collection
                             ? node.SourceParent
                             : node;
        }

        public static void ForGroups(this IEnumerable<IFilter> filters, Action<IFilterGroup> action)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                action(group);
            }
        }

        public static void ForAllGroups(this IEnumerable<IFilter> filters, Action<IFilterGroup> action)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                group.Filters.ForAllGroups(action);
                action(group);
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

        public static void ForNodes(this Dictionary<IToken, List<INode>> nodeDependencies, Tree tree, Action<INode> action)
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
                    var dependencies = tree.GetDependencies(child);
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

        public static IFilterGroup GetParentGroup(this Tree tree, IDepend depend)
        {
            return depend.GroupToken == null
                ? null :
                tree.FilterGroups[depend.GroupToken];
        }

        public static bool IsParent(this Tree tree, IToken child, IToken parent)
        {
            return tree.GetParents(child).Contains(parent);
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

            var dependencies = tree.Nodes[token].Provider.Dependencies.Select(x => x.Token).ToList();
            return new HashSet<IToken>(dependencies.SelectMany(tree.GetParents).Concat(dependencies));
        }
    }
}
