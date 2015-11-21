namespace TestingContextCore.OldImplementation.Logging
{
    internal interface IFailureReporter
    {
        void ReportFailure(FailureCollect collect, int[] startingWeight);
    }
}
