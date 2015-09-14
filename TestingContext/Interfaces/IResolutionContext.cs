namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;

    public interface IResolutionContext<T>
    {
        T Value { get; }

        IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key);
    }
}
