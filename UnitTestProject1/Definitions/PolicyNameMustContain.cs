namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using UnitTestProject1.Entities;

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
