namespace TestingContextCore.Implementation.Sources
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal interface ISource
    {
        EntityDefinition EntityDefinition { get; }

        IEnumerable<IResolutionContext<T>> Resolve<T>(string key);

        bool IsChildOf(ISource source);
    }
}
