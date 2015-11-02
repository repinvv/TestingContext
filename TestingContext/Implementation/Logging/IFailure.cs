namespace TestingContextCore.Implementation.Logging
{
    using System.Collections.Generic;

    internal interface IFailure
    {
        IEnumerable<Definition> Definitions { get; }

        string FilterString { get; }

        string Key { get; }
    }
}
