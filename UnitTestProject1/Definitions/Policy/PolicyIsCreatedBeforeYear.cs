namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

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
            context.For<Policy>(key).Filter(policy => policy.Created.Year < year);
        }
    }
}
