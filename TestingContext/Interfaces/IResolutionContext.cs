namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    public interface IResolutionContext<T>
    {
        T Value { get; }

        IEnumerable<IResolutionContext<TOther>> Get<TOther>(IToken<TOther> token);

        IEnumerable<IResolutionContext<TOther>> Get<TOther>(string name);
    }
}
