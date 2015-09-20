namespace UnitTestProject1.Definitions.Coverage.Assertions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoverageIdMustBe
    {
        private readonly TestingContext context;

        public CoverageIdMustBe(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"coverage(?:\s)?(.*) Id must be (.*)")]
        public void ThenCoverageIdMustBe(string key, int id)
        {
            Assert.AreEqual(id, context.Value<Coverage>(key).Id);
        }
    }
}
