namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyMustHaveId
    {
        private readonly TestingContext context;

        public PolicyMustHaveId(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"policy(?:\s)?(.*) must have id (.*)")]
        public void ThenPolicyMustHaveId(string key, int id)
        {
            Assert.AreEqual(id, context.Value<Policy>(key).Id);
        }
    }
}
