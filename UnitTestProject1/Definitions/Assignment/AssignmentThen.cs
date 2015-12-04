namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers.Exceptions;
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
            Assert.AreEqual(id, context.TestMatcher().Value<Assignment>(key).Id);
        }

        [Then(@"assignment(?:\s)?(.*) must exist")]
        public void ThenAssignmentMustExist(string key)
        {
            Assert.IsNotNull(context.TestMatcher().Value<Assignment>(key));
        }

        [Then(@"for assignment(?:\s)?(.*) with id (.*) there must be provided assignments(?:\s)?(.*) with ids (.*)")]
        public void ThenForAssignmentWithIdThereMustBeProvidedAssignmentsWithIds(string key1, int id, string key2, string ids)
        {
            var assignment1 = context.TestMatcher().All<Assignment>(key1).First(x => x.Value.Id == id);
            var assignments2Ids = assignment1
                .Get<Assignment>(key2)
                .Select(x => x.Value.Id).ToArray();
            var sourceIds = ids.Split(',').Select(int.Parse).ToArray();
            CollectionAssert.AreEquivalent(sourceIds, assignments2Ids);
        }

        [When(@"I try register that assignment(?:\s)?(.*) has more people than assignments(?:\s)?(.*)")]
        public void WhenITryRegisterThatAssignmentHasMorePeopleThanAssignments(string key1, string key2)
        {
            var token1 = context.GetToken<Assignment>(key1);
            var token2 = context.GetToken<Assignment>(key2);
            try
            {
                context.AssignmentHasMorePeopleThanAssignments(token1, token2);
            }
            catch (RegistrationException ex)
            {
                context.Storage.Set(ex);
            }
        }

        [Then(@"i should get an exception with information about assignment(?:\s)?(.*)")]
        public void ThenIShouldGetAnExceptionWithInformationAboutAssignment(string key)
        {
            var ex = context.Storage.Get<RegistrationException>();
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Message.Contains($"Assignment \"{key}\""));
        }
    }
}
