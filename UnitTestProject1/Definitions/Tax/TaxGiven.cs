namespace UnitTestProject1.Definitions.Tax
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class TaxGiven
    {
        private readonly TestingContext context;

        public TaxGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"for insurance(?:\s)?(.*) exists a tax(?:\s)?(.*)")]
        public void GivenForInsuranceExistsATax(string insuranceKey, string taxKey)
        {
            context.Register()
                   .DependsOn<Insurance>(insuranceKey)
                   .Provide(taxKey, insurance => insurance.Taxes)
                   .Exists("TaxExists");
        }

        [Given(@"tax(?:\s)?(.*) amounts to at least (.*)\$")]
        public void GivenTaxAmountsToAtLeast(string key, int amount)
        {
            context.For<Tax>(key)
                   .IsTrue(tax => tax.Amount >= amount);
        }

        [Given(@"tax(?:\s)?(.*) has type '(.*)'")]
        public void GivenTaxBHasType(string key, TaxType type)
        {
            context
                .For<Tax>(key)
                .IsTrue(tax => tax.Type == type);
        }

        [Given(@"average payment per person in coverages(?:\s)?(.*), specified in taxes(?:\s)?(.*) is over (.*)\$")]
        public void GivenAveragePaymentPerPersonInAssignmentsBSpecifiedInTaxIsOver(string assignmentKey, string taxKey, int average)
        {
            context
                .ForCollection<Assignment>(assignmentKey)
                .WithCollection<Tax>(taxKey)
                .Filter((coverages, taxes) => taxes.Sum(x => x.Value.Amount) / coverages.Sum(x => x.Value.HeadCount) > average);
        }
    }
}
