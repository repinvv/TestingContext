namespace TestingContext.LimitedInterface
{
    using System.Collections.Generic;

    public interface ITokenRegister : IHighLevelOperations<ITokenRegister>
    {
        IForToken<T> For<T>(IHaveToken<T> haveToken);

        IForToken<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);
    }
}
