namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    public class PolicyHasCoveredCount
    {
        private readonly TestingContext testingContext;

        public PolicyHasCoveredCount(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Given(@"policy (.*)has at least (.*) people covered")]
        public void GivenPolicyHasAtLeastPeopleCovered(string key, int amount)
        {
            //policyItems[key].AddCondition(policy => policy.Covered != null && policy.Covered.Sum(x => x.Amount) >= amount);
        }
    }
}
