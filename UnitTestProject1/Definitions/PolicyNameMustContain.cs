namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyNameMustContain
    {
        private readonly TestingContext testingContext;

        public PolicyNameMustContain(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Then(@"policy(?:\s)?(.*) name must contain (.*)")]
        public void ThenPolicyNameMustContain(string key, string namepart)
        {
           Assert.IsTrue(testingContext.Value<Policy>(key).Name.Contains(namepart));
        }
    }
}
