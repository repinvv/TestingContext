namespace UnitTestProject1.Definitions.Insurance
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers.Exceptions;
    using UnitTestProject1.Entities;

    [Binding]
    public class InsuranceWhen
    {
        private readonly ITestingContext context;

        public InsuranceWhen(ITestingContext context)
        {
            this.context = context;
        }

        [When(@"i try resolving insurance(?:\s)?(.*)")]
        public void WhenInsuranceResolves(string key)
        {
            try
            {
                var value = context.GetMatcher().All<Insurance>(key);
            }
            catch (RegistrationException ex)
            {
                context.Storage.Set(ex);
            }
        }
    }
}
