namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;

    internal partial class Registration1<T1> : IForName<T1>
    {
        public void IsTrue(string name, Expression<Func<T1, bool>> filter, string file, int line, string member)
            => store.SaveFilterToken(name, inner.IsTrue(filter, file, line, member));

        public IFor<T1, T2> For<T2>(string name) => inner.For(new LazyHaveToken<T2>(() => store.GetToken<T2>(name)));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name) 
            => inner.ForCollection(new LazyHaveToken<T2>(() => store.GetToken<T2>(name)));

        public void Exists<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member) 
            => store.SaveToken(name, inner.Exists(srcFunc, file, line, member));

        public void DoesNotExist<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.DoesNotExist(srcFunc, file, line, member));

        public void Each<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Each(srcFunc, file, line, member));
    }
}
