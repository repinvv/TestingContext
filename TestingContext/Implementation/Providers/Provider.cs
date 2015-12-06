namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextCore.UsefulExtensions;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly TokenStore store;

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            TokenStore store,
            IDiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            this.store = store;
            Dependencies = new IDependency[] { dependency };
        }

        public IDiagInfo DiagInfo { get; }
        public IEnumerable<IDependency> Dependencies { get; }

        public IFilter CollectionValidityFilter { get; set; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource sourceValue = dependency.GetValue(parentContext);
            IEnumerable<T> source;
            try
            {
                source = sourceFunc(sourceValue) ?? Enumerable.Empty<T>();
            }
            catch (Exception ex)
            {
                throw new RegistrationException($"Exception in registered expression", DiagInfo, ex);
            }

            return source
                .Where(x => x != null)
                .Select(x => new ResolutionContext<T>(x, node, parentContext, store))
                .Cache();
        }
    }
}
