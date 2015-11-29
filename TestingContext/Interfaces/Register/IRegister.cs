namespace TestingContextCore.Interfaces.Register
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    public interface IRegister : IRegisterToken, IRegisterName
    {
        IFor<T> For<T>(Func<IToken<T>> getToken);

        IFor<IEnumerable<T>> ForCollection<T>(Func<IToken<T>> getToken);
    }
}
