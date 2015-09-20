namespace UnitTestProject1.Definitions.Coverage
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class ForPolicyExistsACoverage
    {
        private readonly TestingContext context;

        public ForPolicyExistsACoverage(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"for policy(?:\s)?(.*) exists a coverage(?:\s)?(.*)")]
        public void GivenForPolicyExistsACoverage(string policyKey, string coverageKey)
        {
            context
                .ExistsFor<Policy>(policyKey)
                .Provide<Coverage>(coverageKey, policy => policy.Coverages);
        }

    }
}
