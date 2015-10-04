namespace UnitTestProject1.Definitions.Policy.Then
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyNameMustContain
    {
        private readonly TestingContext context;

        public PolicyNameMustContain(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"policy(?:\s)?(.*) name must contain '(.*)'")]
        public void ThenPolicyNameMustContain(string key, string namepart)
        {
            var policy = context.Value<Policy>(key);
            Assert.IsTrue(policy.Name.Contains(namepart));
        }
    }
}
