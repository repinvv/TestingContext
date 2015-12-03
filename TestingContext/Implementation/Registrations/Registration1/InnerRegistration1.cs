namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;
    using static StoreExtension;

    internal class InnerRegistration1<T1>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency;
        private readonly IFilterGroup group;

        public InnerRegistration1(TokenStore store, IDependency<T1> dependency, IFilterGroup group)
        {
            this.store = store;
            this.dependency = dependency;
            this.group = group;
        }

        public IFilterToken IsTrue(Expression<Func<T1, bool>> filterFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, filterFunc);
            var filter = new Filter1<T1>(dependency, filterFunc.Compile(), diagInfo);
            store.RegisterFilter(filter, group);
            return filter.Token;
        }

        public IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken)
        {
            return RegistrationFactory.GetRegistration2(store, dependency, new SingleValueDependency<T2>(haveToken), group);
        }

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken)
        {
            return RegistrationFactory.GetRegistration2(store, dependency, new CollectionValueDependency<T2>(haveToken), group);
        }

        public Declarator<T2> Declare<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            var token = new Token<T2>();
            var provider = new Provider<T1, T2>(dependency, srcFunc, store);
            return new Declarator<T2>(store, token, provider, group);
        }
    }
}
