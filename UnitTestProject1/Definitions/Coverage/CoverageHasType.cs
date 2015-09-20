namespace UnitTestProject1.Definitions.Coverage
{
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoverageHasType
    {
        private readonly TestingContext context;

        public CoverageHasType(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"coverage(?:\s)?(.*) has type '(.*)'")]
        public void GivenCoverageHasType(string key, CoverageType type)
        {
            context.For<Coverage>(key).Filter(coverage => coverage.Type == type);
        }
    }
}
