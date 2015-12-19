namespace TestingContext.Interface
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;

    public interface IResolutionContext<T>
    {
        T Value { get; }

        IEnumerable<IResolutionContext<TOther>> Get<TOther>(IToken<TOther> token);

        IEnumerable<IResolutionContext<TOther>> Get<TOther>(string name);
    }
}
