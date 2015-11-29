namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal partial class Registration1<T1> : IForToken<T1>
    {
        public IFilterToken IsTrue(Expression<Func<T1, bool>> filter, string file, int line, string member)
            => inner.IsTrue(filter, file, line, member);

        public IFor<T1, T2> For<T2>(IToken<T2> token) => inner.For(new HaveToken<T2>(token));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(IToken<T2> token) => inner.ForCollection(new HaveToken<T2>(token));

        public IToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member) 
            => inner.Exists(srcFunc, file, line, member);

        public IToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member) 
            => inner.DoesNotExist(srcFunc, file, line, member);

        public IToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member) 
            => inner.Each(srcFunc, file, line, member);
    }
}
