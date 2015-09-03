namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class PolicyHasCoveredRows
    {
        public PolicyHasCoveredRows()
        {
        }

        [Given(@"policy (.*)has at least (.*) covered rows")]
        public void GivenPolicyHasAtLeastCoveredRows(string key, int amount)
        {
            //policyItems[key].AddCondition(policy => policy.Covered != null && policy.Covered.Count >= amount);
        }
    }
}
