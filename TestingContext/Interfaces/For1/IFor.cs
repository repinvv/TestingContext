namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    public interface IFor<T1> : IForToken<T1>, IForName<T1>
    {
        IFor<T1, T2> For<T2>(Func<IToken<T2>> getToken);

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(Func<IToken<T2>> getToken);
    }
}
