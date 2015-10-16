namespace UnitTestProject1.Definitions.Tax
{
    using System.Linq;
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
                   .For<Insurance>(insuranceKey)
                   .Exists(taxKey, insurance => insurance.Taxes);
        }

        [Given(@"tax(?:\s)?(.*) amounts to at least (.*)\$")]
        public void GivenTaxAmountsToAtLeast(string key, int amount)
        {
            context.Register()
                   .For<Tax>(key)
                   .IsTrue(tax => tax.Amount >= amount);
        }

        [Given(@"tax(?:\s)?(.*) has type '(.*)'")]
        public void GivenTaxBHasType(string key, TaxType type)
        {
            context
                .Register()
                .For<Tax>(key)
                .IsTrue(tax => tax.Type == type);
        }

        [Given(@"average payment per person in coverages(?:\s)?(.*), specified in taxes(?:\s)?(.*) is over (.*)\$")]
        public void GivenAveragePaymentPerPersonInAssignmentsBSpecifiedInTaxIsOver(string assignmentKey, string taxKey, int average)
        {
            context
                .Register()
                .ForAll<Assignment>(assignmentKey)
                .ForAll<Tax>(taxKey)
                .IsTrue((coverages, taxes) => taxes.Sum(x => x.Amount) / coverages.Sum(x => x.HeadCount) > average);
        }
    }
}
