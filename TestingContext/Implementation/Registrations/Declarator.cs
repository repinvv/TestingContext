namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using static StoreExtension;

    internal class Declarator<T>
    {
        private readonly TokenStore store;
        private readonly IToken<T> token;
        private readonly IProvider provider;
        private readonly IFilterGroup group;

        public Declarator(TokenStore store, IToken<T> token, IProvider provider, IFilterGroup group)
        {
            this.store = store;
            this.token = token;
            this.provider = provider;
            this.group = group;
        }

        public IHaveToken<T> Exists(IDiagInfo diagInfo)
        {
            store.RegisterCvFilter(CreateExistsFilter(token, group, diagInfo), group);
            store.RegisterProvider(provider, token, group);
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
            store.RegisterCvFilter(CreateCvFilter(filterFunc, token, group, diagInfo), group);
            store.RegisterProvider(provider, token, group);
            return new HaveToken<T>(token);
        }
    }
}
