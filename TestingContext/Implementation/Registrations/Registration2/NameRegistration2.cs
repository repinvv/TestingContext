namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;

    internal partial class Registration2<T1, T2> : IForName<T1, T2>
    {
        public void IsTrue(string name, Expression<Func<T1, T2, bool>> filter, string file, int line, string member)
            => store.SaveFilterToken(name, inner.IsTrue(filter, file, line, member));

        public void Exists<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => store.SaveToken(name, inner.Exists(srcFunc, file, line, member));

        public void DoesNotExist<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => store.SaveToken(name, inner.DoesNotExist(srcFunc, file, line, member));

        public void Each<T3>(string name, Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member) 
            => store.SaveToken(name, inner.Each(srcFunc, file, line, member));
    }
}
