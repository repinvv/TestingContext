namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;

    internal class GroupProvider : IProvider 
    {
        private readonly TokenStore store;

        public GroupProvider(IEnumerable<IDependency> dependencies, IFilterGroup group, TokenStore store, IDiagInfo diagInfo)
        {
            Dependencies = dependencies;
            Group = group;
            DiagInfo = diagInfo;
            this.store = store;
        }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilterGroup Group { get; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            return new[] { new ResolutionContext<IFilterGroup>(Group, node, parentContext, store) };
        }
    }
}
