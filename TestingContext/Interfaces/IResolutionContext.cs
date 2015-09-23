namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;

    public interface IResolutionContext<T>
    {
        bool MeetsConditions { get; }

        T Value { get; }

        IEnumerable<IResolutionContext<TChild>> Get<TChild>(string key);
    }
}
