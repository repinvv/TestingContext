namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;

    public interface IResolutionContext<T>
    {
        bool MeetsConditions { get; }

        T Value { get; }

        IEnumerable<IResolutionContext<T2>> Get<T2>(string key);
    }
}
