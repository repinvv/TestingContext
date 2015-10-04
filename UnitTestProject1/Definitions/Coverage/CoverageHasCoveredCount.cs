namespace UnitTestProject1.Definitions.Coverage
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoverageHasCoveredCount
    {
        private readonly TestingContext context;

        public CoverageHasCoveredCount(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"coverage(?:\s)?(.*) has at least (.*) people covered")]
        public void GivenCoverageHasAtLeastPeopleCovered(string key, int headCount)
        {
            context.For<Coverage>(key)
                .Filter(coverage => coverage.HeadCount >= headCount);
        }
    }
}
