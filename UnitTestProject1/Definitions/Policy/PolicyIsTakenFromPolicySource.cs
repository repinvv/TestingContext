namespace UnitTestProject1.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;
    using UnitTestProject1.TestSource;

    [Binding]
    public class PolicyIsTakenFromPolicySource
    {
        private readonly TestingContext context;

        public PolicyIsTakenFromPolicySource(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"policy(?:\s)?(.*) is taken from policiesSource")]
        public void GivenPolicyIsTakenFromPoliciesSource(string key)
        {
            context
                .Register()
                .Provide(key, x => PoliciesSource.Policies);
            //testingContext
            //    .DoesNotExistFor<Policy>(key)
            //    .TakesSourceFrom<Policy>(key)
            //    .Provide(key, p => p.Covered);

            //testingContext
            //    .For<Policy>(key)
            //    .Filter(p => p.Created.Year > 2014);
            //testingContext
            //    .For<Covered>(key)
            //    .For<Policy>(key)
            //    .Filter((c, p) => c.Amount > p.Created.Year);

            //var a = testingContext
            //    .All<Covered>(key);
            //var b = testingContext
            //    .Value<Covered>(key);
        }
    }
}
