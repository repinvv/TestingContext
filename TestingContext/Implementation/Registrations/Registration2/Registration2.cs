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

        public Registration2(TokenStore store, InnerRegistration2<T1,T2> inner)
        {
            this.inner = inner;
            this.store = store;
        }

        public IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter, string file, int line, string member)
            => inner.IsTrue(filter, file, line, member);

        IDeclare<T3> IFor<T1, T2>.Declare<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            return null;
        }

        IDeclareSingle<T3> IFor<T1, T2>.DeclareSingle<T3>(Func<T1, T2, T3> srcFunc)
        {
            return null;
        }

        public ITokenDeclare<T3> Declare<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            return null;
        }

        public ITokenDeclareSingle<T3> DeclareSingle<T3>(Func<T1, T2, T3> srcFunc)
        {
            return null;
        }
    }
}
