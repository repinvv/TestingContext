namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
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
        public static IFilterGroup GetParentGroup(this TreeContext context, IDepend depend)
        {
            return depend.ParentGroupToken == null
                ? null
                : context.Groups[depend.ParentGroupToken];
        }

        public static bool IsParent(this TreeContext context, IToken child, IToken parent)
        {
            return context.GetParents(child).Contains(parent);
        }

        public static HashSet<IToken> GetParents(this TreeContext context, IToken token)
        {
            return context.Parents.GetOrAdd(token, () => FindParents(context, token));
        }

        private static HashSet<IToken> FindParents(TreeContext context, IToken token)
        {
            if (token == context.Store.RootToken)
            {
                return new HashSet<IToken>();
            }

            var dependencies = context.Tree.Nodes[token].Provider.Dependencies.Select(x => x.Token).ToList();
            return new HashSet<IToken>(dependencies.SelectMany(context.GetParents).Concat(dependencies));
        }

        public static INode GetGroupNode(this TreeContext context, IFilterGroup group)
        {
            var token = group?.NodeToken;
            if (token == null)
            {
                return null;
            }
            INode node;
            return context.Tree.Nodes.TryGetValue(token, out node) ? node : null;
        }

        public static bool IsCvFilter(this TreeContext context, IFilter filter)
        {
            return context.Store.CvFilters.Contains(filter.FilterInfo.FilterToken);
        }

        // can be used after the tree is built
        public static INode GetDependencyNode(this TreeContext context, IDependency dependency)
        {
            var node = context.Tree.GetNode(dependency.Token);
            return dependency.Type == DependencyType.CollectionValidity
                       ? node.Parent
                       : dependency.Type == DependencyType.Collection
                             ? node.SourceParent
                             : node;
        }
    }
}
