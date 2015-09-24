namespace UnitTestProject1.Definitions.Policy.When
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    internal class PolicyResolves
    {
        private readonly TestingContext context;

        public PolicyResolves(TestingContext context)
        {
            this.context = context;
        }

        [When(@"policy(?:\s)?(.*) resolves")]
        public void WhenPolicyBResolves(string key)
        {
            var value = context.All<Policy>(key);
        }
    }
}
