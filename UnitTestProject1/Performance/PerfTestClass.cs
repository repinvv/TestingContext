namespace UnitTestProject1.Performance
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class PerfTest
    {
        private readonly ITestingContext context;

        private List<Model> models;

        public PerfTest(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have a model with (.*) branches containing (.*) items each")]
        public void GivenIHaveSetUpAModelWithContainingOfItems(int branches, int items)
        {
            var watch = new Stopwatch();
            models = ModelGenerator.GenerateModels(branches, items).ToList();
            watch.Stop();
            Debug.WriteLine("generate time " + watch.Elapsed);
            context.Exists("model", () => models);
            for (int i = 0; i < branches; i++)
            {
                var i1 = i;
                var token = context.For<Model>("model").Exists(model => model.Branches[i1]);
                context.For<Item>(token).IsTrue(x => x.Valid);
            }
        }

        [Then(@"nothing should be found and the search time is under (.*) sec")]
        public void ThenNothingShouldBeFoundAndTheSearchTimeIsUnderSec(int seconds)
        {
            var watch = Stopwatch.StartNew();
            var match = context.GetMatcher().All<Model>("model").FirstOrDefault();
            watch.Stop();
            Debug.WriteLine("search in " + watch.Elapsed);
            Assert.IsNull(match);
            Assert.IsTrue(watch.ElapsedMilliseconds < 1000 * seconds);
        }
    }
}
