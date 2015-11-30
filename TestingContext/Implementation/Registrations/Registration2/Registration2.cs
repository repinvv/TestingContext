namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;

    internal class Registration2<T1, T2> : IFor<T1, T2>
    {
        private readonly InnerRegistration2<T1, T2> inner;
        private readonly TokenStore store;

        public Registration2(TokenStore store, InnerRegistration2<T1, T2> inner)
        {
            this.inner = inner;
            this.store = store;
        }

        public IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter, string file, int line, string member)
            => inner.IsTrue(filter, file, line, member);

        public IHaveToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).Exists(file, line, member);

        public IHaveToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).DoesNotExist(file, line, member);

        public IHaveToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).Each(file, line, member);

        public void Exists<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).Exists(file, line, member).Token, file, line, member);

        public void DoesNotExist<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).DoesNotExist(file, line, member).Token, file, line, member);

        public void Each<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).Each(file, line, member).Token, file, line, member);
    }
}
