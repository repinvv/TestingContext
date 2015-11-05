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
            context.Register()
                   .For<Insurance>(key)
                   .IsTrue(insurance => insurance.Created.Year == year);
        }

        [Given(@"insurance(?:\s)?(.*) is taken from policiesSource")]
        public void GivenInsuranceIsTakenFromPoliciesSource(string key)
        {
            context.Register()
                   .Items(key, () => PoliciesSource.Insurances);
        }

        [Given(@"for insurance(?:\s)?(.*) there is no insurance(?:\s)?(.*) in policiesSource that meet requirements")]
        public void GivenForInsuranceThereIsNoInsuranceInPoliciesSourceThatMeetRequirements(string key1, string key2)
        {
            context.Register().For<Insurance>(key1)
                .DoesNotExist(key2, p => PoliciesSource.Insurances);
        }
    }
}
