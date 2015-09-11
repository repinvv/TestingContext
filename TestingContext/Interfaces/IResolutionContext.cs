namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Resolution;

    internal interface IResolutionContext
    {
        IResolution Resolve(Definition definition);
    }

    public interface IResolutionContext<T>
    {
        T Value { get; }

        IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key);
    }
}
