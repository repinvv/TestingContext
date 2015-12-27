namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;

    internal class GroupProvider : IProvider 
    {
        private readonly IFilterGroup group;
        private readonly TokenStore store;

        public GroupProvider(IEnumerable<IDependency> dependencies,
            IFilterGroup group, IFilterToken parentGroupToken,
            TokenStore store,
            IDiagInfo diagInfo)
        {
            Dependencies = dependencies;
            ParentGroupToken = parentGroupToken;
            DiagInfo = diagInfo;
            this.group = group;
            this.store = store;
        }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilterToken ParentGroupToken { get; }

        public bool IsNegative { get; set; } = true;

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            return new[] { new ResolutionContext<IFilterGroup>(group, node, parentContext, store) };
        }
    }
}
