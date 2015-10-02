namespace UnitTestProject1.Definitions.Coverage
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoversLessPeopleThanMaximumDependendtsInPolicy
    {
        private readonly TestingContext context;

        public CoversLessPeopleThanMaximumDependendtsInPolicy(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"coverage(?:\s)?(.*) covers less people than maximum dependendts specified in policy(?:\s)?(.*)")]
        public void GivenCoverageCoversLessPeopleThanMaximumDependendtsSpecifiedInPolicy(string coverageKey, string policyKey)
        {
            context
                .For<Coverage>(coverageKey)
                .With<Policy>(policyKey)
                .Filter((coverage, policy) => coverage.HeadCount < policy.MaximumDependents);
        }
    }
}
