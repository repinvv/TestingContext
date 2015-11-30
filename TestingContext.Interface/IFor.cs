namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IFor<T1> : IForToken<T1>
    {
        new IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        new IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IFor<T1, T2> For<T2>(string name,
                             [CallerFilePath] string file = "",
                             [CallerLineNumber] int line = 0,
                             [CallerMemberName] string member = "");

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name,
                                                    [CallerFilePath] string file = "",
                                                    [CallerLineNumber] int line = 0,
                                                    [CallerMemberName] string member = "");

        new IDeclare<T2> Declare<T2>(Func<T1, IEnumerable<T2>> srcFunc);

        new IDeclareSingle<T2> DeclareSingle<T2>(Func<T1, T2> srcFunc);
    }
}
