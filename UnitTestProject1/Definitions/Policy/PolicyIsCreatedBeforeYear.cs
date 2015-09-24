namespace UnitTestProject1.Definitions.Policy
{
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    public class PolicyIsCreatedBeforeYear
    {
        private readonly TestingContext context;

        public PolicyIsCreatedBeforeYear(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"policy(?:\s)?(.*) is created before year (.*)")]
        public void GivenPolicyIsCreatedBeforeYear(string key, int year)
        {
            context.For<Entities.Policy>(key).Filter(policy => policy.Created.Year < year);
        }
    }
}
