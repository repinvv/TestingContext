namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextInterface;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal class Registration2<T1, T2> : HighLevelRegistrations, IFor<T1, T2>
    {
        private readonly InnerRegistration2<T1, T2> inner;
        private readonly TokenStore store;

        public Registration2(TokenStore store, InnerRegistration2<T1, T2> inner, InnerHighLevelRegistration innerHighLevel)
            : base(innerHighLevel)
        {
            this.inner = inner;
            this.store = store;
        }

        public IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, T2, bool> filter)
            => inner.IsTrue(diagInfo, filter);

        public IHaveToken<T3> Exists<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => inner.Declare(diagInfo, srcFunc).Exists(diagInfo);

        public IHaveToken<T3> DoesNotExist<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => inner.Declare(diagInfo, srcFunc).DoesNotExist(diagInfo);

        public IHaveToken<T3> Each<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => inner.Declare(diagInfo, srcFunc).Each(diagInfo);

        public void Exists<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => store.SaveToken(diagInfo, name, inner.Declare(diagInfo, srcFunc).Exists(diagInfo).Token);

        public void DoesNotExist<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => store.SaveToken(diagInfo, name, inner.Declare(diagInfo, srcFunc).DoesNotExist(diagInfo).Token);

        public void Each<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc) 
            => store.SaveToken(diagInfo, name, inner.Declare(diagInfo, srcFunc).Each(diagInfo).Token);

        private static Func<T1, T2, IEnumerable<T3>> SingleFunc<T3>(Expression<Func<T1, T2, T3>> srcFunc)
        {
            var compiled = srcFunc.Compile();
            return (x, y) =>
            {
                var item = compiled(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            };
        }
    }
}
