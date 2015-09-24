namespace TestingContextCore.Implementation.Logging
{
    internal class ExistsConditionFailure : IFailure
    {
        public string FailureString { get; } = "x => x.Any(y => y.MeetsCondition)";
    }
}
