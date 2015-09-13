namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyMustExist
    {
        private readonly TestingContext testingContext;

        public PolicyMustExist(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Then(@"policy(?:\s)?(.*) must exist")]
        public void ThenPolicyAMustExist(string key)
        {
            Assert.IsNotNull(testingContext.Value<Policy>(key));
        }
    }
}
