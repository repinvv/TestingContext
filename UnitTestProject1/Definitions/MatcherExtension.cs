namespace UnitTestProject1.Definitions
{
    using TestingContext.Interface;

    internal static class MatcherExtension
    {
        public static IMatcher TestMatcher(this ITestingContext context)
        {
            return context.Storage.Get<IMatcher>("matcher");
        }

        public static IMatcher SetTestMatcher(this ITestingContext context)
        {
            var matcher = context.GetMatcher();
            context.Storage.Set(matcher, "matcher");
            return matcher;
        }
    }
}
