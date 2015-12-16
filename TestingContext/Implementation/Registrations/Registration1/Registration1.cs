namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Registrations.HighLevel;

    internal class Registration1<T1> : HighLevelRegistrations, IFor<T1>
    {
        private readonly TokenStore store;
        internal InnerRegistration1<T1> Inner { get; }

        public Registration1(TokenStore store, InnerRegistration1<T1> inner, InnerHighLevelRegistration innerHighLevel) 
            : base(innerHighLevel)
        {
            this.store = store;
            Inner = inner;
        }

        public IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, bool> filter)
            => Inner.IsTrue(diagInfo, filter);

        #region For
        IFor<T1, T2> IFor<T1>.For<T2>(IHaveToken<T2> haveToken) => Inner.For(haveToken);
        IFor<T1, IEnumerable<T2>> IFor<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => Inner.ForCollection(haveToken);
        IForToken<T1, T2> IForToken<T1>.For<T2>(IHaveToken<T2> haveToken) => Inner.For(haveToken);
        IForToken<T1, IEnumerable<T2>> IForToken<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => Inner.ForCollection(haveToken);

        public IFor<T1, T2> For<T2>(string name, string file, int line, string member)
            => Inner.For(store.GetHaveToken<T2>(DiagInfo.Create(file, line, member), name));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name, string file, int line, string member)
            => Inner.ForCollection(store.GetHaveToken<T2>(DiagInfo.Create(file, line, member), name));
        #endregion

        #region declare
        public IHaveToken<T2> Exists<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc) 
            => Inner.Declare(diagInfo, srcFunc).Exists(diagInfo);

        public IHaveToken<T2> DoesNotExist<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc) 
            => Inner.Declare(diagInfo, srcFunc).DoesNotExist(diagInfo);

        public IHaveToken<T2> Each<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc) 
            => Inner.Declare(diagInfo, srcFunc).Each(diagInfo);

        public void Exists<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc) 
            => store.SaveToken(diagInfo, name, Inner.Declare(diagInfo, srcFunc).Exists(diagInfo).Token);

        public void DoesNotExist<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc) 
            => store.SaveToken(diagInfo, name, Inner.Declare(diagInfo, srcFunc).DoesNotExist(diagInfo).Token);

        public void Each<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc) 
            => store.SaveToken(diagInfo, name, Inner.Declare(diagInfo, srcFunc).Each(diagInfo).Token);
        #endregion

        private static Func<T1, IEnumerable<T2>> SingleFunc<T2>(Expression<Func<T1, T2>> srcFunc)
        {
            var compiled = srcFunc.Compile();
            return x =>
            {
                var item = compiled(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            };
        }
    }
}
