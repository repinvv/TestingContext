namespace TestingContextCore.Implementation.Logging
{
    using TestingContextCore.Implementation.Registrations;

    internal static class LoggingExtension
    {
        public static void LogEmptyResult(this RegistrationStore store, Definition definition, IFailureReporter reporter)
        {
            var collect = new FailureCollect(store);
            reporter.ReportFailure(collect, new int[0]);
            collect.LogFailure();
        }
    }
}
