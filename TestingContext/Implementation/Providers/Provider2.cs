namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class Provider2<TSource1, TSource2, T> : IProvider
    {
        private readonly IDependency<TSource1> dependency1;
        private readonly IDependency<TSource2> dependency2;
        private readonly Func<TSource1, TSource2, IEnumerable<T>> sourceFunc;
        private readonly TokenStore store;
        private readonly Dictionary<Tuple<TSource1, TSource2>, IEnumerable<T>> resolves 
            = new Dictionary<Tuple<TSource1, TSource2>, IEnumerable<T>>();

        public Provider2(IDependency<TSource1> dependency1, 
            IDependency<TSource2> dependency2,
            Func<TSource1, TSource2, IEnumerable<T>> sourceFunc,
            TokenStore store,
            IFilterToken parentGroupToken,
            IDiagInfo diagInfo)
        {
            ParentGroupToken = parentGroupToken;
            DiagInfo = diagInfo;
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.sourceFunc = sourceFunc;
            this.store = store;
            Dependencies = new IDependency[] { dependency1, dependency2 };
        }

        public IFilterToken ParentGroupToken { get; set; }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilter CollectionValidityFilter { get; set; }

        public bool IsNegative { get; set; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource1 sourceValue1 = dependency1.GetValue(parentContext);
            TSource2 sourceValue2 = dependency2.GetValue(parentContext);
            IEnumerable<T> source;
            try
            {
                source = resolves.GetOrAdd(new Tuple<TSource1, TSource2>(sourceValue1, sourceValue2),
                                           () => sourceFunc(sourceValue1, sourceValue2) ?? Enumerable.Empty<T>());
            }
            catch (Exception ex)
            {
                throw new RegistrationException($"Exception in registered expression a provider of type {typeof(T).Name}", DiagInfo, ex);
            }
            
            return source
                .Where(x => x != null)
                .Select(x => new ResolutionContext<T>(x, node, parentContext, store))
                .Cache();
        }
    }
}
