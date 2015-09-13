namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class PolicyIsCreatedBeforeYear
    {
        public PolicyIsCreatedBeforeYear()
        {
        }

        [Given(@"policy (.*)is created before year (.*)")]
        public void GivenPolicyIsCreatedBeforeYear(string key, int year)
        {
            //policyItems[key].AddCondition(policy => policy.Created.Year < year);
        }
    }
}
