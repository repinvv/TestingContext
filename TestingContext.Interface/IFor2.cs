namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IFor<T1, T2> : IForToken<T1, T2>
    {
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
