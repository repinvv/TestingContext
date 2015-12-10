namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.PublicMembers;

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

        public IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter, string file, int line, string member)
            => inner.IsTrue(filter, file, line, member);

        public IHaveToken<T3> Exists<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return inner.Declare(srcFunc.Compile(), diagInfo).Exists(diagInfo);
        }

        public IHaveToken<T3> DoesNotExist<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return inner.Declare(srcFunc.Compile(), diagInfo).DoesNotExist(diagInfo);
        }

        public IHaveToken<T3> Each<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return inner.Declare(srcFunc.Compile(), diagInfo).Each(diagInfo);
        }

        public IHaveToken<T3> Is<T3>(Expression<Func<T1, T2, T3>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return inner.Declare(SingleFunc(srcFunc), diagInfo).Exists(diagInfo);
        }

        public IHaveToken<T3> IsNot<T3>(Expression<Func<T1, T2, T3>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            return inner.Declare(SingleFunc(srcFunc), diagInfo).DoesNotExist(diagInfo);
        }

        public void Exists<T3>(string name, Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Declare(srcFunc.Compile(), diag).Exists(diag).Token, diag);
        }

        public void DoesNotExist<T3>(string name, Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Declare(srcFunc.Compile(), diag).DoesNotExist(diag).Token, diag);
        }

        public void Each<T3>(string name, Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Declare(srcFunc.Compile(), diag).Each(diag).Token, diag);
        }

        public void Is<T3>(string name, Expression<Func<T1, T2, T3>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Declare(SingleFunc(srcFunc), diag).Exists(diag).Token, diag);
        }

        public void IsNot<T3>(string name, Expression<Func<T1, T2, T3>> srcFunc, string file, int line, string member)
        {
            var diag = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Declare(SingleFunc(srcFunc), diag).DoesNotExist(diag).Token, diag);
        }

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
