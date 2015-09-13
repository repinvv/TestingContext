namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class PolicyNameMustContain
    {
        public PolicyNameMustContain()
        {
        }

        [Then(@"policy (.*)name must contain (.*)")]
        public void ThenPolicyNameMustContain(string key, string namepart)
        {
            //Assert.IsTrue(policyItems[key].Value.Name.Contains(namepart));
        }
    }
}
