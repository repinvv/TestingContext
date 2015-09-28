namespace UnitTestProject1.Definitions.Coverage
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoveragesHaveCoveredPeople
    {
        private readonly TestingContext context;

        public CoveragesHaveCoveredPeople(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"coverages(?:\s)?(.*) have covered people")]
        public void GivenCoveragesBHaveCoveredPeople(string key)
        {
            context.ForCollection<Coverage>(key)
                   .Filter(coverages => coverages.Sum(x => x.Value.HeadCount) > 0);
        }
    }
}
