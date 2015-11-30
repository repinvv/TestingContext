namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForToken<T1, T2>
    {
        IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter,
                    [CallerFilePath] string file = "",
                    [CallerLineNumber] int line = 0,
                    [CallerMemberName] string member = "");

        ITokenDeclare<T3> Declare<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc);

        ITokenDeclareSingle<T3> DeclareSingle<T3>(Func<T1, T2, T3> srcFunc);
    }
}
