namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IFor<T1>
    {
        IHaveFilterToken IsTrue(Expression<Func<T1, bool>> filter,
                                [CallerFilePath] string file = "",
                                [CallerLineNumber] int line = 0,
                                [CallerMemberName] string member = "");

        IExtendedFor<T1, T2> For<T2>(Func<ITestingContext, IToken<T2>> getToken);

        IExtendedFor<T1, IEnumerable<T2>> ForCollection<T2>(Func<ITestingContext, IToken<T2>> getToken);

        IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc,
                                  [CallerLineNumber] int line = 0,
                                  [CallerFilePath] string file = "",
                                  [CallerMemberName] string member = "");

        IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc,
                                        [CallerLineNumber] int line = 0,
                                        [CallerFilePath] string file = "",
                                        [CallerMemberName] string member = "");

        IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc,
                                [CallerLineNumber] int line = 0,
                                [CallerFilePath] string file = "",
                                [CallerMemberName] string member = "");

        IHaveToken<T2> Is<T2>(Func<T1, T2> srcFunc,
                              [CallerLineNumber] int line = 0,
                              [CallerFilePath] string file = "",
                              [CallerMemberName] string member = "");

        IHaveToken<T2> IsNot<T2>(Func<T1, T2> srcFunc,
                                 [CallerLineNumber] int line = 0,
                                 [CallerFilePath] string file = "",
                                 [CallerMemberName] string member = "");
    }

    public interface IExtendedFor<T1> : IFor<T1>
    {
        IExtendedFor<T1, T2> For<T2>(IHaveToken<T2> haveToken);
        IExtendedFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);
        IExtendedFor<T1, T2> For<T2>(string name);
        IExtendedFor<T1, IEnumerable<T2>> ForCollection<T2>(string name);
        void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        void Is<T2>(Func<T1, T2> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        void IsNot<T2>(Func<T1, T2> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
    }
}
