namespace UnitTestProject1.Definitions.Insurance
{
    using TechTalk.SpecFlow;
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;

    [Binding]
    public class InsuranceGiven
    {
        private readonly ITestingContext context;

        public InsuranceGiven(ITestingContext context)
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

        [Given(@"insurance(?:\s)?(.*) is taken from insurancesSource")]
        public void GivenInsuranceIsTakenFromPoliciesSource(string key)
        {
            context.Register()
                   .Exists<Insurance>(() => InsurancesSource.Insurances, key);
        }

        [Given(@"for insurance(?:\s)?(.*) there is no insurance(?:\s)?(.*) in insurancesSource that meet requirements")]
        public void GivenForInsuranceThereIsNoInsuranceInPoliciesSourceThatMeetRequirements(string key1, string key2)
        {
            context.Register()
                   .For<Insurance>(key1)
                   .DoesNotExist<Insurance>(p => InsurancesSource.Insurances, key2);
        }

        [Given(@"insuranse(?:\s)?(.*) matches high level OR condition")]
        public void GivenInsuranseMatchesHiLevelORCondition(string key)
        {
            context.Register()
                   .Or(x => x.InsuranceHasFederalTax(key),
                       x => x.InsuranceHasMaximumDependents(key),
                       x => x.InsuranceHasDependentAssignment(key));
        }

        [Given(@"insuranse(?:\s)?(.*) matches high level NOT condition")]
        public void GivenInsuranseMatchesHiLevelNOTCondition(string key)
        {
            context.Register()
                   .Not(x =>
                   {
                       x.InsuranceHasMaximumDependents(key);
                       x.InsuranceHasDependentAssignment(key);
                       x.InsuranceHasFederalTax(key);
                   });
        }}
}
