namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Resolution;

    internal interface IResolutionContext
    {
        IResolution Resolve(Definition definition);

        IResolutionContext GetContext(Definition contextDef);
    }

    public interface IResolutionContext<T>
    {
        bool MeetsConditions { get; }

        T Value { get; }

        IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key);
    }
}
