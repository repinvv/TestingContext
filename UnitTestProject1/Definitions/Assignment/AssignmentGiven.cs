namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class AssignmentGiven
    {
        private readonly TestingContext context;

        public AssignmentGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"assignment(?:\s)?(.*) has at least (.*) people covered")]
        public void GivenAssignmentHasAtLeastPeopleCovered(string key, int headCount)
        {
            context.Register()
                   .For<Assignment>(key)
                   .IsTrue(assignment => assignment.HeadCount >= headCount);
        }

        [Given(@"assignment(?:\s)?(.*) has type '(.*)'")]
        public void GivenAssignmentHasType(string key, AssignmentType type)
        {
            context.Register()
                .For<Assignment>(key)
                .IsTrue(assignment => assignment.Type == type);
        }

        [Given(@"assignments(?:\s)?(.*) have covered people")]
        public void GivenAssignmentsHaveCoveredPeople(string key)
        {
            context.Register()
                   .ForAll<Assignment>(key)
                   .IsTrue(assignments => assignments.Sum(x => x.HeadCount) > 0);
        }

        [Given(@"assignment(?:\s)?(.*) covers less people than maximum dependendts specified in insurance(?:\s)?(.*)")]
        public void GivenAssignmentCoversLessPeopleThanMaximumDependendtsSpecifiedInInsurance(string assignmentKey, string insuranceKey)
        {
            context.Register()
                   .For<Assignment>(assignmentKey)
                   .For<Insurance>(insuranceKey)
                   .IsTrue((assignment, insurance) => assignment.HeadCount < insurance.MaximumDependents);
        }

        [Given(@"for insurance(?:\s)?(.*) exists an assignment(?:\s)?(.*)")]
        public void GivenForInsuranceExistsAAssignment(string insuranceKey, string assignmentKey)
        {
            context.Register()
                   .For<Insurance>(insuranceKey)
                   .Exists(assignmentKey, insurance => insurance.Assignments);
        }

        [Given(@"there is no suitable assignment(?:\s)?(.*)")]
        public void GivenThereIsNoSuitableAssignment(string key)
        {
            context.InvertCollectionValidity<Assignment>(key);
        }

        [Given(@"assignment(?:\s)?(.*) is created at the same day as assignment(?:\s)?(.*)")]
        public void GivenAssignmentIsCreatedAtTheSameDayAsAssignment(string key1, string key2)
        {
            context.Register()
                   .For<Assignment>(key1)
                   .For<Assignment>(key2)
                   .IsTrue((assignment1, assignment2) => assignment1.Created.Date == assignment2.Created.Date);
        }
    }
}
