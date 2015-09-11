namespace TestingContextCore.Implementation.Sources
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface ISource
    {
        Definition Definition { get; }

        ISource Root { get; }

        bool IsChildOf(ISource source);

        IResolution Resolve(IResolutionContext parentContext);
    }
}
