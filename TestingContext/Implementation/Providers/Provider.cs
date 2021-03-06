﻿namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;
    using TestingContextLimitedInterface.UsefulExtensions;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly TokenStore store;
        private readonly Dictionary<TSource, IEnumerable<T>> resolves = new Dictionary<TSource, IEnumerable<T>>();

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            TokenStore store,
            IFilterToken parentGroupToken,
            IDiagInfo diagInfo)
        {
            ParentGroupToken = parentGroupToken;
            DiagInfo = diagInfo;
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            this.store = store;
            Dependencies = new IDependency[] { dependency };
        }

        public IFilterToken ParentGroupToken { get; set; }

        public IDiagInfo DiagInfo { get; }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilter CollectionValidityFilter { get; set; }

        public bool IsNegative { get; set; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource sourceValue = dependency.GetValue(parentContext);
            IEnumerable<T> source;
            try
            {
                source = resolves.GetOrAdd(sourceValue, () => sourceFunc(sourceValue) ?? Enumerable.Empty<T>());
            }
            catch (Exception ex)
            {
                throw new RegistrationException($"Exception in registered expression for a provider of type {typeof(T).Name}", DiagInfo, ex);
            }

            return source
                .Where(x => x != null)
                .Select(x => new ResolutionContext<T>(x, node, parentContext, store))
                .Cache();
        }
    }
}
