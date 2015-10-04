namespace UnitTestProject1.Definitions.Tax
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class ForPolicyExistsATax
    {
        private readonly TestingContext context;

        public ForPolicyExistsATax(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"for policy(?:\s)?(.*) exists a tax(?:\s)?(.*)")]
        public void GivenForPolicyExistsATax(string policyKey, string taxKey)
        {
            context.Register()
                   .DependsOn<Policy>(policyKey)
                   .Provide(taxKey, policy => policy.Taxes)
                   .Exists("TaxExists");
        }
    }
}
