namespace TestingContextCore.Implementation.Registrations
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface ISource
    {
        EntityDefinition EntityDefinition { get; }

        IEnumerable<IResolutionContext<T>> Resolve<T>(string key);
    }
}
