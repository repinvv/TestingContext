namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForName<T1, T2>
    {
        void IsTrue(string name,
            Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Exists<T3>(string name,
            Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void DoesNotExist<T3>(string name,
            Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Each<T3>(string name,
            Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
