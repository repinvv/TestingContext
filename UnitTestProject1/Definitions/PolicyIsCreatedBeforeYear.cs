namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyIsCreatedBeforeYear
    {
        private readonly TestingContext testingContext;

        public PolicyIsCreatedBeforeYear(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Given(@"policy(?:\s)?(.*) is created before year (.*)")]
        public void GivenPolicyIsCreatedBeforeYear(string key, int year)
        {
            testingContext.For<Policy>(key).Filter(policy => policy.Created.Year < year);
        }
    }
}
