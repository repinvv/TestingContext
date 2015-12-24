namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;

    internal class GroupProvider : IProvider 
    {
        private readonly IFilterGroup group;
        private readonly Tree tree;

        public GroupProvider(IEnumerable<IDependency> dependencies, IFilterGroup group, Tree tree, IDiagInfo diagInfo)
        {
            Dependencies = dependencies;
            DiagInfo = diagInfo;
            this.group = group;
            this.tree = tree;
        }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilterToken ParentGroupToken => tree.GetParentGroup(group)?.FilterInfo.FilterToken;

        public bool IsNegative { get; set; } = true;

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            return new[] { new ResolutionContext<IFilterGroup>(group, node, parentContext, tree.Store) };
        }
    }
}
