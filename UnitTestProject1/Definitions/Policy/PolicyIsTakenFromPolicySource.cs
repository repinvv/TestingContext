namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;

    [Binding]
    public class PolicyIsTakenFromPolicySource
    {
        private readonly TestingContext context;

        public PolicyIsTakenFromPolicySource(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"policy(?:\s)?(.*) is taken from policiesSource")]
        public void GivenPolicyIsTakenFromPoliciesSource(string key)
        {
            context
                .Register()
                .Provide(key, x => PoliciesSource.Policies);
        }
    }
}
