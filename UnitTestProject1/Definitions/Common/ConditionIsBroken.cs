namespace UnitTestProject1.Definitions.Common
{
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    public class ConditionIsBroken
    {
        private readonly TestingContext context;

        public ConditionIsBroken(TestingContext context)
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
