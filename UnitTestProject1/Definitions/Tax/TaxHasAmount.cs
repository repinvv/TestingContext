namespace UnitTestProject1.Definitions.Tax
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class TaxHasAmount
    {
        private readonly TestingContext context;

        public TaxHasAmount(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"tax(?:\s)?(.*) amounts to at least (.*)\$")]
        public void GivenTaxAmountsToAtLeast(string key, int amount)
        {
            context.For<Tax>(key)
                   .Filter(tax => tax.Amount >= amount);
        }
    }
}
