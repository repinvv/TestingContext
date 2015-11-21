namespace UnitTestProject1.Definitions.Common
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using TestingContextCore.Interfaces;

    [Binding]
    public class ConditionIsBroken
    {
        private readonly ITestingContext context;

        public ConditionIsBroken(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"condition '(.*)' is broken")]
        public void GivenConditionIsBroken(string key)
        {
            context.InvertFilter(key);
        }
    }
}
