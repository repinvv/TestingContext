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
            context.For<Assignment>(key)
                .IsTrue(assignment => assignment.HeadCount >= headCount);
        }

        [Given(@"assignment(?:\s)?(.*) has type '(.*)'")]
        public void GivenAssignmentHasType(string key, AssignmentType type)
        {
            context.For<Assignment>(key).IsTrue(assignment => assignment.Type == type);
        }

        [Given(@"coverages(?:\s)?(.*) have covered people")]
        public void GivenAssignmentsHaveCoveredPeople(string key)
        {
            context.ForCollection<Assignment>(key)
                   .IsTrue(coverages => coverages.Sum(x => x.Value.HeadCount) > 0);
        }

        [Given(@"assignment(?:\s)?(.*) covers less people than maximum dependendts specified in insurance(?:\s)?(.*)")]
        public void GivenAssignmentCoversLessPeopleThanMaximumDependendtsSpecifiedInInsurance(string assignmentKey, string insuranceKey)
        {
            context
                .For<Assignment>(assignmentKey)
                .With<Insurance>(insuranceKey)
                .Filter((assignment, insurance) => assignment.HeadCount < insurance.MaximumDependents);
        }

        [Given(@"for insurance(?:\s)?(.*) exists an assignment(?:\s)?(.*)")]
        public void GivenForInsuranceExistsAAssignment(string insuranceKey, string assignmentKey)
        {
            context.Register()
                   .DependsOn<Insurance>(insuranceKey)
                   .Provide(assignmentKey, insurance => insurance.Assignments)
                   .Exists("AssignmentExists");
        }
    }
}
