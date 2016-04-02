namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal class GroupProvider : IProvider 
    {
        private readonly IFilterGroup group;
        private readonly TokenStore store;

        public GroupProvider(IEnumerable<IDependency> dependencies,
            IFilterGroup group,
            TokenStore store,
            IDiagInfo diagInfo)
        {
            Dependencies = dependencies;
            DiagInfo = diagInfo;
            this.group = group;
            this.store = store;
        }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilterToken ParentGroupToken => group.ParentGroupToken;

        public bool IsNegative { get; set; } = true;

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            return new[] { new ResolutionContext<IFilterGroup>(group, node, parentContext, store) };
        }
    }
}
