namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using UnitTestProject1.Entities;

    [Binding]
    public class AssignmentThen
    {
        private readonly ITestingContext context;

        public AssignmentThen(ITestingContext context)
        {
            this.context = context;
        }

        [Then(@"assignment(?:\s)?(.*) Id must be (.*)")]
        public void ThenAssignmentIdMustBe(string key, int id)
        {
            Assert.AreEqual(id, context.Value<Assignment>(key).Id);
        }

        [Then(@"assignment(?:\s)?(.*) must exist")]
        public void ThenAssignmentMustExist(string key)
        {
            Assert.IsNotNull(context.Value<Assignment>(key));
        }

        [Then(@"for assignment(?:\s)?(.*) with id (.*) there must be provided assignments(?:\s)?(.*) with ids (.*)")]
        public void ThenForAssignmentWithIdThereMustBeProvidedAssignmentsWithIds(string key1, int id, string key2, string ids)
        {
            var assignment1 = context.All<Assignment>(key1).First(x => x.Value.Id == id);
            var assignments2Ids = assignment1
                .Get<Assignment>(key2)
                .Select(x => x.Value.Id).ToArray();
            var sourceIds = ids.Split(',').Select(int.Parse).ToArray();
            CollectionAssert.AreEquivalent(sourceIds, assignments2Ids);
        }

    }
}
