namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyHasCoveredRows
    {
        private readonly TestingContext testingContext;

        public PolicyHasCoveredRows(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Given(@"policy(?:\s)?(.*) has at least (.*) covered rows")]
        public void GivenPolicyHasAtLeastCoveredRows(string key, int amount)
        {
            testingContext.For<Policy>(key).Filter(policy => policy.Covered != null && policy.Covered.Count >= amount);
        }
    }
}
