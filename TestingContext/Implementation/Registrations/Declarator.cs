namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using static StoreExtension;

    internal class Declarator<T>
    {
        private readonly TokenStore store;
        private readonly IToken<T> token;
        private readonly IProvider provider;
        private readonly FilterGroupRegistration group;
        private readonly int priority;

        public Declarator(TokenStore store, IToken<T> token, IProvider provider, FilterGroupRegistration group, int priority)
        {
            this.store = store;
            this.token = token;
            this.provider = provider;
            this.group = group;
            this.priority = priority;
        }

        public IHaveToken<T> Exists(IDiagInfo diagInfo)
        {
            var info = new FilterInfo(diagInfo, new FilterToken(), group?.GroupToken, priority, store.NextId);
            var dependency = new CollectionDependency(token);
            var filterReg = new FilterRegistration(() => new ExistsFilter(dependency, info));
            store.RegisterCvFilter(filterReg, group, info.Token);
            store.RegisterProvider(provider, token);
            return new HaveToken<T>(token);
        }

        public IHaveToken<T> DoesNotExist(IDiagInfo diagInfo)
        {
            return CreateDefinition(x => !x.Any(y => y.MeetsConditions), diagInfo);
        }

        public IHaveToken<T> Each(IDiagInfo diagInfo)
        {
            return CreateDefinition(x => x.GroupBy(item => item)
                                          .All(grp => grp.Any(item => item.MeetsConditions)), diagInfo);
        }

        internal IHaveToken<T> CreateDefinition(Func<IEnumerable<IResolutionContext>, bool> filterFunc,
            IDiagInfo diagInfo)
        {
            var dependency = new CollectionDependency(token);
            var info = new FilterInfo(diagInfo, new FilterToken(), group?.GroupToken, priority, store.NextId);
            var filterReg = new FilterRegistration(() => new Filter1<IEnumerable<IResolutionContext>>(dependency, filterFunc, info));
            store.RegisterCvFilter(filterReg, group, info.Token);
            provider.IsNegative = true;
            store.RegisterProvider(provider, token);
            return new HaveToken<T>(token);
        }
    }
}
