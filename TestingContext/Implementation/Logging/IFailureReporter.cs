namespace TestingContextCore.Implementation.Logging
{
    internal interface IFailureReporter
    {
        void ReportFailure(FailureCollect collect, int[] startingWeight);
    }
}
