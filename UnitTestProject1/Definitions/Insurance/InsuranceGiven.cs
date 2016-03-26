namespace UnitTestProject1.Definitions.Insurance
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;

    [Binding]
    public class InsuranceGiven
    {
        private readonly ITestingContext context;

        public InsuranceGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"insurance(?:\s)?(.*) is created in year (.*)")]
        public void GivenInsuranceIsCreatedInYear(string key, int year)
        {
            context.For<Insurance>(key)
                   .IsTrue(insurance => insurance.Created.Year == year);
        }

        [Given(@"insurance(?:\s)?(.*) is taken from insurancesSource")]
        public void GivenInsuranceIsTakenFromPoliciesSource(string key)
        {
            context.Exists(key, () => InsurancesSource.Insurances);
        }

        [Given(@"for insurance(?:\s)?(.*) there is no insurance(?:\s)?(.*) in insurancesSource that meet requirements")]
        public void GivenForInsuranceThereIsNoInsuranceInPoliciesSourceThatMeetRequirements(string key1, string key2)
        {
            context.For<Insurance>(key1)
                   .DoesNotExist(key2, p => InsurancesSource.Insurances);
        }

        [Given(@"insuranse(?:\s)?(.*) matches high level OR condition")]
        public void GivenInsuranseMatchesHiLevelORCondition(string key)
        {
            var token = context.GetToken<Insurance>(key);
            context.Either(x => x.InsuranceHasFederalTax(token),
                       x => x.InsuranceHasMaximumDependents(token),
                       x => x.InsuranceHasDependentAssignment(token));
        }

        [Given(@"insuranse(?:\s)?(.*) matches high level NOT condition")]
        public void GivenInsuranseMatchesHiLevelNOTCondition(string key)
        {
            var token = context.GetToken<Insurance>(key);
            context.Not(x =>
                   {
                       x.InsuranceHasMaximumDependents(token);
                       x.InsuranceHasDependentAssignment(token);
                       x.InsuranceHasFederalTax(token);
                   });
        }
    }
}
