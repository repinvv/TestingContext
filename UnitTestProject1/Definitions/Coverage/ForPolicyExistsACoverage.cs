namespace UnitTestProject1.Definitions.Coverage
{
    using System.Linq;
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
                .Register()
                .DependsOn<Policy>(policyKey)
                .Provide(coverageKey, policy => policy.Coverages)
                .Filter(x => x.Any());
        }

    }
}
