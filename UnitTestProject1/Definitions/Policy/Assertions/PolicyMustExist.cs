namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyMustExist
    {
        private readonly TestingContext context;

        public PolicyMustExist(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"policy(?:\s)?(.*) must exist")]
        public void ThenPolicyAMustExist(string key)
        {
            Assert.IsNotNull(context.Value<Policy>(key));
        }
    }
}
