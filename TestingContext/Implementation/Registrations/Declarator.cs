namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;
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

        public IHaveToken<T> Exists(string file, int line, string member) 
            => CreateDefinition(x => x.Any(y => y.MeetsConditions), file, line, member);

        public IHaveToken<T> DoesNotExist(string file, int line, string member) 
            => CreateDefinition(x => !x.Any(y => y.MeetsConditions), file, line, member);

        public IHaveToken<T> Each(string file, int line, string member) 
            => CreateDefinition(x => x.GroupBy(item => item).All(grp => grp.Any(item => item.MeetsConditions)), file, line, member);

        internal IHaveToken<T> CreateDefinition(Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpr,
            string file,
            int line,
            string member)
        {
            var diag = DiagInfo.Create(file, line, member, filterExpr);
            provider.CollectionValidityFilter = CreateCvFilter(filterExpr.Compile(), token, group, diag);
            store.RegisterProvider(provider, token, group);
            return new HaveToken<T>(token);
        }
    }
}
