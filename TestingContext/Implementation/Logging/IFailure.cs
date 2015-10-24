namespace TestingContextCore.Implementation.Logging
{
    internal interface IFailure
    {
        string FilterString { get; }

        string Key { get; }

        bool Inverted { get; }
    }
}
