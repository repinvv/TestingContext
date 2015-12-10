namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.PublicMembers;

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

        public IFilterToken IsTrue(Expression<Func<T1, bool>> filter, string file, int line, string member)
            => Inner.IsTrue(filter, file, line, member);

        #region For
        IFor<T1, T2> IFor<T1>.For<T2>(IHaveToken<T2> haveToken) => Inner.For(haveToken);
        IFor<T1, IEnumerable<T2>> IFor<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => Inner.ForCollection(haveToken);
        IForToken<T1, T2> IForToken<T1>.For<T2>(IHaveToken<T2> haveToken) => Inner.For(haveToken);
        IForToken<T1, IEnumerable<T2>> IForToken<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => Inner.ForCollection(haveToken);

        public IFor<T1, T2> For<T2>(string name, string file, int line, string member)
            => Inner.For(store.GetHaveToken<T2>(name, file, line, member));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name, string file, int line, string member)
            => Inner.ForCollection(store.GetHaveToken<T2>(name, file, line, member));
        #endregion

        #region declare
        public IHaveToken<T2> Exists<T2>(Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return Inner.Declare(srcFunc.Compile(), diagInfo).Exists(diagInfo);
        }

        public IHaveToken<T2> DoesNotExist<T2>(Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return Inner.Declare(srcFunc.Compile(), diagInfo).DoesNotExist(diagInfo);
        }

        public IHaveToken<T2> Each<T2>(Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return Inner.Declare(srcFunc.Compile(), diagInfo).Each(diagInfo);
        }

        public IHaveToken<T2> Is<T2>(Expression<Func<T1, T2>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return Inner.Declare(SingleFunc(srcFunc), diagInfo).Exists(diagInfo);
        }
        
        public IHaveToken<T2> IsNot<T2>(Expression<Func<T1, T2>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return Inner.Declare(SingleFunc(srcFunc), diagInfo).DoesNotExist(diagInfo);
        }

        public void Exists<T2>(string name, Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, Inner.Declare(srcFunc.Compile(), diag).Exists(diag).Token, diag);
        }

        public void DoesNotExist<T2>(string name, Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, Inner.Declare(srcFunc.Compile(), diag).DoesNotExist(diag).Token, diag);
        }

        public void Each<T2>(string name, Expression<Func<T1, IEnumerable<T2>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, Inner.Declare(srcFunc.Compile(), diag).Each(diag).Token, diag);
        }

        public void Is<T2>(string name, Expression<Func<T1, T2>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, Inner.Declare(SingleFunc(srcFunc), diag).Exists(diag).Token, diag);
        }

        public void IsNot<T2>(string name, Expression<Func<T1, T2>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, Inner.Declare(SingleFunc(srcFunc), diag).DoesNotExist(diag).Token, diag);
        }
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
