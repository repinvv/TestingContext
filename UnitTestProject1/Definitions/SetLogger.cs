namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    internal class SetLoggerHook
    {
        private readonly TestingContext context;
        private readonly TestLogger logger;

        public SetLoggerHook(TestingContext context, TestLogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [BeforeScenario]
        public void SetLogger()
        {
            context.OnSearchFailure += logger.OnSearchFailure;
        }
    }
}
