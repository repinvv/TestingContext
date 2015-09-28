namespace UnitTestProject1.Definitions.Policy
{
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    public class PolicyIsCreatedInYear
    {
        private readonly TestingContext context;

        public PolicyIsCreatedInYear(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"policy(?:\s)?(.*) is created in year (.*)")]
        public void GivenPolicyIsCreatedBeforeYear(string key, int year)
        {
            context.For<Entities.Policy>(key).Filter(policy => policy.Created.Year == year);
        }
    }
}
