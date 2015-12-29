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

    internal static class TreeBuildingExtensions
    {
        //public static INode GetCvFilterNode(this Tree tree, IFilter filter)
        //{
        //    return tree.GetNode(filter.Dependencies.First().Token);
        //}

        //public static INode GetNode(this Tree tree, IToken token)
        //{
        //    return tree.Nodes.SafeGet(token);
        //}

        //public static INode GetNode(this Tree tree, IFilterGroup group)
        //{
        //    return tree.GetNode(group?.NodeToken);
        //}

        //public static bool IsCvFilter(this TreeContext context, IFilter filter)
        //{
        //    return context.Store.CvFilters.Contains(filter.FilterInfo.FilterToken);
        //}

        //// can be used after the tree is built
        //public static INode GetDependencyNode(this Tree tree, IDependency dependency)
        //{
        //    var node = tree.GetNode(dependency.Token);
        //    return dependency.Type == DependencyType.CollectionValidity
        //               ? node.Parent
        //               : dependency.Type == DependencyType.Collection
        //                     ? node.SourceParent
        //                     : node;
        //}

        public static void ForGroups(this IEnumerable<IFilter> filters, Action<IFilterGroup> action)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                action(group);
            }
        }

        //public static void ForDependencies(this IDepend depend, Action<IDependency, IDependency> action)
        //{
        //    var dependencies = depend.Dependencies.ToArray();
        //    for (int i = 0; i < dependencies.Length; i++)
        //    {
        //        for (int j = i + 1; j < dependencies.Length; j++)
        //        {
        //            if (dependencies[i].Token != dependencies[j].Token)
        //            {
        //                action(dependencies[i], dependencies[j]);
        //            }
        //        }
        //    }
        //}

        //public static void ForNodes(this Dictionary<IToken, List<INode>> nodeDependencies, TreeContext context, Action<INode> action)
        //{
        //    var nodesQueue = new Queue<INode>(new[] { context.Tree.Root });
        //    var assigned = new HashSet<IToken> { context.Tree.Root.Token };
        //    while (nodesQueue.Any())
        //    {
        //        var current = nodesQueue.Dequeue();
        //        List<INode> children;
        //        if (!nodeDependencies.TryGetValue(current.Token, out children))
        //        {
        //            continue;
        //        }

        //        foreach (var child in children)
        //        {
        //            var dependencies = context.GetDependencies(child);
        //            if (!dependencies.All(x => assigned.Contains(x.Token)))
        //            {
        //                continue;
        //            }

        //            action(child);
        //            assigned.Add(child.Token);
        //            nodesQueue.Enqueue(child);
        //        }
        //    }
        //}

        //public static IFilterGroup GetParentGroup(this TreeContext context, IDepend depend)
        //{
        //    return depend.ParentGroupToken == null
        //        ? null :
        //        context.Groups[depend.ParentGroupToken];
        //}

        //public static bool IsParent(this TreeContext context, IToken child, IToken parent)
        //{
        //    return context.GetParents(child).Contains(parent);
        //}

        //public static HashSet<IToken> GetParents(this TreeContext context, IToken token)
        //{
        //    return context.Parents.GetOrAdd(token, () => FindParents(context, token));
        //}

        //private static HashSet<IToken> FindParents(TreeContext context, IToken token)
        //{
        //    if (token == context.Store.RootToken)
        //    {
        //        return new HashSet<IToken>();
        //    }

        //    var dependencies = context.Tree.Nodes[token].Provider.Dependencies.Select(x => x.Token).ToList();
        //    return new HashSet<IToken>(dependencies.SelectMany(context.GetParents).Concat(dependencies));
        //}
    }
}
