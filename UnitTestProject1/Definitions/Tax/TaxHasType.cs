namespace UnitTestProject1.Definitions.Tax
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class TaxHasType
    {
        private readonly TestingContext context;

        public TaxHasType(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"tax(?:\s)?(.*) has type '(.*)'")]
        public void GivenTaxBHasType(string key, TaxType type)
        {
            context
                .For<Tax>(key)
                .Filter(tax => tax.Type == type);
        }
    }
}
