namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRegistration<TSource>
    {
        IRegistration<T> DependsOn<T>(string key);

        IRegistration<T> Resolves<T>(string key);

        IFor<IEnumerable<IResolutionContext<T>>> Provide<T>(string key, Func<TSource, IEnumerable<T>> srcFunc);

        IFor<IEnumerable<IResolutionContext<T>>> Provide<T>(string key, Func<IEnumerable<T>> srcFunc);

        IFor<IEnumerable<IResolutionContext<T>>> ProvideItem<T>(string key, Func<TSource, T> srcFunc);

        IFor<IEnumerable<IResolutionContext<T>>> ProvideItem<T>(string key, Func<T> srcFunc);
    }
}
