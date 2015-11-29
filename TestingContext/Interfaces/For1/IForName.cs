namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForName<T1>
    {
        void IsTrue(string name,
            Expression<Func<T1, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T1, T2> For<T2>(string name);

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name);

        void Exists<T2>(string name,
            Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void DoesNotExist<T2>(string name,
            Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Each<T2>(string name,
            Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
