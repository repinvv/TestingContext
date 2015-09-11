namespace TestingContextCore.Implementation.Sources
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface ISource
    {
        IResolution Resolve(IResolutionContext parentContext);
    }
}
