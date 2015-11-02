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
            context.Register()
                   .For<Tax>(key)
                   .IsTrue(tax => tax.Type == type);
        }

        [Given(@"average payment per person in assignments(?:\s)?(.*), specified in taxes(?:\s)?(.*) is over (.*)\$")]
        public void GivenAveragePaymentPerPersonInAssignmentsBSpecifiedInTaxIsOver(string assignmentKey, string taxKey, int average)
        {
            context.Register()
                   .ForAll<Assignment>(assignmentKey)
                   .ForAll<Tax>(taxKey)
                   .IsTrue((assignments, taxes) => taxes.Sum(x => x.Amount) / assignments.Sum(x => x.HeadCount) > average);
        }

        [Given(@"there is no suitable tax(?:\s)?(.*)")]
        public void GivenThereIsNoSuitableTax(string key)
        {
            context.InvertCollectionValidity<Tax>(key);
        }

        [Given(@"assignment(?:\s)?(.*) is created at the same day as tax(?:\s)?(.*)")]
        public void GivenAssignmentIsCreatedAtTheSameDayAsTax(string assignmentKey, string taxKey)
        {
            context.Register()
                   .For<Assignment>(assignmentKey)
                   .For<Tax>(taxKey)
                   .IsTrue((assignment, tax) => assignment.Created.Date == tax.Created.Date);
        }
    }
}
