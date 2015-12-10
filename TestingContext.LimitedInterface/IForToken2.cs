namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForToken<T1, T2> : IHighLevelOperations<ITokenRegister>
    {
        IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter,
                            [CallerFilePath] string file = "",
                            [CallerLineNumber] int line = 0,
                            [CallerMemberName] string member = "");

        IHaveToken<T3> Exists<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                                  [CallerFilePath] string file = "",
                                  [CallerLineNumber] int line = 0,
                                  [CallerMemberName] string member = "");

        IHaveToken<T3> DoesNotExist<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                                        [CallerFilePath] string file = "",
                                        [CallerLineNumber] int line = 0,
                                        [CallerMemberName] string member = "");

        IHaveToken<T3> Each<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
                                [CallerFilePath] string file = "",
                                [CallerLineNumber] int line = 0,
                                [CallerMemberName] string member = "");

        IHaveToken<T3> Is<T3>(Expression<Func<T1, T2, T3>> srcFunc,
                                        [CallerFilePath] string file = "",
                                        [CallerLineNumber] int line = 0,
                                        [CallerMemberName] string member = "");

        IHaveToken<T3> IsNot<T3>(Expression<Func<T1, T2, T3>> srcFunc,
                                             [CallerFilePath] string file = "",
                                             [CallerLineNumber] int line = 0,
                                             [CallerMemberName] string member = "");
    }
}
