namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;
    using TestingContextInterface;
    using UnitTestProject1.Entities;

    [Binding]
    public class AssignmentGiven
    {
        private readonly ITestingContext context;

        public AssignmentGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"assignment(?:\s)?(.*) has at least (.*) people covered")]
        public void GivenAssignmentHasAtLeastPeopleCovered(string key, int headCount)
        {
            context.For<Assignment>(key)
                   .IsTrue(assignmen => assignmen.HeadCount >= headCount);
        }

        [Given(@"assignment(?:\s)?(.*) has type '(.*)'")]
        public void GivenAssignmentHasType(string key, AssignmentType type)
        {
            context.For<Assignment>(key)
                   .IsTrue(assignment => assignment.Type == type);
        }

        [Given(@"assignments(?:\s)?(.*) have covered people")]
        public void GivenAssignmentsHaveCoveredPeople(string key)
        {
            context.ForCollection<Assignment>(key)
                   .IsTrue(assignments => assignments.Sum(x => x.HeadCount) > 0);
        }

        [Given(@"assignment(?:\s)?(.*) covers less people than maximum dependendts specified in insurance(?:\s)?(.*)")]
        public void GivenAssignmentCoversLessPeopleThanMaximumDependendtsSpecifiedInInsurance(string assignmentKey, string insuranceKey)
        {
            context.For<Assignment>(assignmentKey)
                   .For<Insurance>(insuranceKey)
                   .IsTrue((assignment, insurance) => assignment.HeadCount < insurance.MaximumDependents);
        }

        [Given(@"for insurance(?:\s)?(.*) exists an assignment(?:\s)?(.*)")]
        public void GivenForInsuranceExistsAssignment(string insuranceKey, string assignmentKey)
        {
            context.For<Insurance>(insuranceKey)
                   .Exists(assignmentKey, insurance => insurance.Assignments);
        }

        [Given(@"there is no suitable assignment(?:\s)?(.*)")]
        public void GivenThereIsNoSuitableAssignment(string key)
        {
            context.Inversion.InvertCollectionValidity<Assignment>(context.GetToken<Assignment>(key));
        }

        [Given(@"assignment(?:\s)?(.*) is created at the same day as assignment(?:\s)?(.*)")]
        public void GivenAssignmentIsCreatedAtTheSameDayAsAssignment(string key1, string key2)
        {
            context.For<Assignment>(key1)
                   .For<Assignment>(key2)
                   .IsTrue((assignment1, assignment2) => assignment1.Created.Date == assignment2.Created.Date);
        }

        [Given(@"assignments(?:\s)?(.*) cover (.*) people total")]
        public void GivenAssignmentsCoverPeopleTotal(string key, int covered)
        {
            context.ForCollection<Assignment>(key)
                   .IsTrue(assignments => assignments.Sum(x => x.HeadCount) == covered);
        }

        [Given(@"assignments(?:\s)?(.*) cover more people than assignments(?:\s)?(.*)")]
        public void GivenAssignmentsCoverMorePeopleThanAssignments(string key1, string key2)
        {
            context.ForCollection<Assignment>(key1)
                   .ForCollection<Assignment>(key2)
                   .IsTrue((assignments1, assignments2) => assignments1.Sum(x => x.HeadCount) > assignments2.Sum(x => x.HeadCount));
        }

        [Given(@"assignments(?:\s)?(.*) cover as much or more people than assignments(?:\s)?(.*)")]
        public void GivenAssignmentsCoverAsMuchOrMorePeopleThanAssignments(string key1, string key2)
        {
            context.ForCollection<Assignment>(key1)
                   .ForCollection<Assignment>(key2)
                   .IsTrue((assignments1, assignments2) => assignments1.Sum(x => x.HeadCount) >= assignments2.Sum(x => x.HeadCount));
        }

        [Given(@"assignment(?:\s)?(.*) has one item in insurances(?:\s)?(.*). Do not pay any attention to how dumb it sounds")]
        public void GivenAssignmentHasSomePoliciesSoNotPayAnyAttentionToHowDumbItSounds(string assignmentKey, string insuranceKey)
        {
            context.For<Assignment>(assignmentKey)
                   .ForCollection<Insurance>(insuranceKey)
                   .IsTrue((assignment, insurances) => insurances.Count() == 1);
        }

        [Given(@"all assignments(?:\s)?(.*) in insurance(?:\s)?(.*) meet following criteria")]
        public void GivenAllAssignmentsInInsuranceMeetFollowingCriteria(string assignmentKey, string insuranceKey)
        {
            context.For<Insurance>(insuranceKey)
                   .Each(assignmentKey, x => x.Assignments);
        }

        [Given(@"assignment(?:\s)?(.*) has more people than assignments(?:\s)?(.*)")]
        public void GivenAssignmentHasMorePeopleThanAssignments(string key1, string key2)
        {
            var token1 = context.GetToken<Assignment>(key1);
            var token2 = context.GetToken<Assignment>(key2);
            context.AssignmentHasMorePeopleThanAssignments(token1, token2);
        }
    }
}
