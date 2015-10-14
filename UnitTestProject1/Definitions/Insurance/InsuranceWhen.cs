namespace UnitTestProject1.Definitions.Insurance
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class InsuranceWhen
    {
        private readonly TestingContext context;

        public InsuranceWhen(TestingContext context)
        {
            this.context = context;
        }

        [When(@"insurance(?:\s)?(.*) resolves")]
        public void WhenInsuranceBResolves(string key)
        {
            var value = context.All<Insurance>(key);
        }
    }
}
