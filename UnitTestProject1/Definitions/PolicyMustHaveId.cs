namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class PolicyMustHaveId
    {
        public PolicyMustHaveId()
        {
        }

        [Then(@"policy (.*)must have id (.*)")]
        public void ThenPolicyMustHaveId(string key, int id)
        {
            //Assert.AreEqual(id, policyItems[key].Value.Id);
        }
    }
}
