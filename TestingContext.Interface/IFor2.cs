namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IFor<T1, T2> : IForToken<T1, T2>
    {
        void Exists<T3>(string name,
                        Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                        [CallerFilePath] string file = "",
                        [CallerLineNumber] int line = 0,
                        [CallerMemberName] string member = "");

        void DoesNotExist<T3>(string name,
                              Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                              [CallerFilePath] string file = "",
                              [CallerLineNumber] int line = 0,
                              [CallerMemberName] string member = "");

        void Each<T3>(string name,
                      Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                      [CallerFilePath] string file = "",
                      [CallerLineNumber] int line = 0,
                      [CallerMemberName] string member = "");

        void Is<T3>(string name,
                              Expression<Func<T1, T2, T3>> srcFunc,
                              [CallerFilePath] string file = "",
                              [CallerLineNumber] int line = 0,
                              [CallerMemberName] string member = "");

        void IsNot<T3>(string name,
                                    Expression<Func<T1, T2, T3>> srcFunc,
                                    [CallerFilePath] string file = "",
                                    [CallerLineNumber] int line = 0,
                                    [CallerMemberName] string member = "");

        IFilterToken Not(Action<IRegister> action,
 [CallerFilePath] string file = "",
 [CallerLineNumber] int line = 0,
 [CallerMemberName] string member = "");

        IFilterToken Either(Action<IRegister> action,
                Action<IRegister> action2,
                Action<IRegister> action3 = null,
                Action<IRegister> action4 = null,
                Action<IRegister> action5 = null,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "");

        IFilterToken Xor(Action<IRegister> action,
                 Action<IRegister> action2,
                 [CallerFilePath] string file = "",
                 [CallerLineNumber] int line = 0,
                 [CallerMemberName] string member = "");
    }
}
