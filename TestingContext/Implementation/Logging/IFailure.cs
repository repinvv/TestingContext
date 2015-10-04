namespace TestingContextCore.Implementation.Logging
{
    internal interface IFailure
    {
        string FailureString { get; }

        string Key { get; }

        bool Inverted { get; }
    }
}
