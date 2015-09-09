namespace TestingContextCore.Implementation.Sources
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface ISource
    {
        EntityDefinition EntityDefinition { get; }

        bool IsChildOf(ISource source);

        IEnumerable<IResolutionContext<T>> RootResolve<T>(string key);

        IResolution Resolve(object parentValue);
    }
}
