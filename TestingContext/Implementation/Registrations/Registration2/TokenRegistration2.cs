namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal partial class Registration2<T1, T2> : IForToken<T1, T2>
    {
        public IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter, string file, int line, string member) 
            => inner.IsTrue(filter, file, line, member);

        public IToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => inner.Exists(srcFunc, file, line, member);

        public IToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => inner.DoesNotExist(srcFunc, file, line, member);

        public IToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => inner.Each(srcFunc, file, line, member);
    }
}
