namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;

    public interface IFailure
    {
        IEnumerable<string> Definitions { get; }

        string FilterString { get; }

        string Key { get; }
    }
}
