namespace UnitTestProject1.Definitions.Coverage.Then
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class CoverageMustExist
    {
        private readonly TestingContext context;

        public CoverageMustExist(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"coverage(?:\s)?(.*) must exist")]
        public void ThenCoverageMustExist(string key)
        {
            Assert.IsNotNull(context.Value<Coverage>(key));
        }

    }
}
