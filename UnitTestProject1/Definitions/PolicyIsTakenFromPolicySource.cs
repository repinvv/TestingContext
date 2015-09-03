namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;
    using TestingContextCore;

    [Binding]
    public class PolicyIsTakenFromPolicySource
    {
        private readonly TestingContext testingContext;

        public PolicyIsTakenFromPolicySource(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Given(@"Policy "+ DefinitionConstants.NameOrEmpty + "is taken from policiesSource")]
        public void GivenPolicyIsTakenFromPoliciesSource(string key)
        {
            testingContext
                .Independent()
                .Source(key, () => PoliciesSource.Policies);
            testingContext
                .DoesNotExistFor<Policy>(key)
                .Source(key, p => p.Covered);

            testingContext
                .For<Policy>(key)
                .Filter(p => p.Created.Year > 2014);
            testingContext
                .For<Covered>(key)
                .For<Policy>(key)
                .Filter((c, p) => c.Amount > p.Created.Year);

            var a = testingContext
                .All<Covered>(key);
            var b = testingContext
                .Value<Covered>(key);
        }
    }
}
