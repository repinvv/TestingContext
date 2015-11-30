namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;

    public interface IFor<T1, T2> : IForToken<T1, T2>
    {
        new IDeclare<T3> Declare<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc);

        new IDeclareSingle<T3> DeclareSingle<T3>(Func<T1, T2, T3> srcFunc);
    }
}
