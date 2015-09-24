namespace TestingContextCore.Implementation.Logging
{
    internal class CustomFailure : IFailure
    {
        public CustomFailure(string customFailure)
        {
            FailureString = customFailure;
        }

        public string FailureString { get; }
    }
}
