namespace UnitTestProject1.Definitions.Insurance
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;

    [Binding]
    public class InsuranceGiven
    {
        private readonly TestingContext context;

        public InsuranceGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"insurance(?:\s)?(.*) is created in year (.*)")]
        public void GivenInsuranceIsCreatedInYear(string key, int year)
        {
            context.For<Insurance>(key).IsTrue(insurance => insurance.Created.Year == year);
        }

        [Given(@"insurance(?:\s)?(.*) is taken from policiesSource")]
        public void GivenInsuranceIsTakenFromPoliciesSource(string key)
        {
            context
                .Register()
                .Provide(key, x => PoliciesSource.Insurances);
        }
    }
}
