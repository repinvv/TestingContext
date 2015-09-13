namespace UnitTestProject1.Definitions
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyHasCoveredCount
    {
        private readonly TestingContext testingContext;

        public PolicyHasCoveredCount(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Given(@"policy(?:\s)?(.*) has at least (.*) people covered")]
        public void GivenPolicyHasAtLeastPeopleCovered(string key, int amount)
        {
            testingContext.For<Policy>(key).Filter(p => p.Covered != null && p.Covered.Sum(x => x.Amount) >= amount);
        }
    }
}
