namespace UnitTestProject1.Definitions.Assignment
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class AssignmentThen
    {
        private readonly TestingContext context;

        public AssignmentThen(TestingContext context)
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
    }
}
