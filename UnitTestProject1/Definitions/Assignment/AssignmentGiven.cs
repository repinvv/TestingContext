namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;

    [Binding]
    public class AssignmentGiven
    {
        private readonly ITestingContext context;

        public AssignmentGiven(ITestingContext context)
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
            context.Register().ForCollection<Assignment>(key)
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
        public void GivenForInsuranceExistsAssignment(string insuranceKey, string assignmentKey)
        {
            context.Register()
                   .For<Insurance>(insuranceKey)
                   .Exists<Assignment>(insurance => insurance.Assignments, assignmentKey);
        }

        [Given(@"there is no suitable assignment(?:\s)?(.*)")]
        public void GivenThereIsNoSuitableAssignment(string key)
        {
            context.InvertCollectionValidity(context.GetToken<Assignment>(key));
        }

        [Given(@"assignment(?:\s)?(.*) is created at the same day as assignment(?:\s)?(.*)")]
        public void GivenAssignmentIsCreatedAtTheSameDayAsAssignment(string key1, string key2)
        {
            context.Register()
                   .For<Assignment>(key1)
                   .For<Assignment>(key2)
                   .IsTrue((assignment1, assignment2) => assignment1.Created.Date == assignment2.Created.Date);
        }

        [Given(@"assignments(?:\s)?(.*) cover (.*) people total")]
        public void GivenAssignmentsCoverPeopleTotal(string key, int covered)
        {
            context.Register()
                   .ForCollection<Assignment>(key)
                   .IsTrue(assignments => assignments.Sum(x => x.HeadCount) == covered);
        }

        [Given(@"assignments(?:\s)?(.*) cover more people than assignments(?:\s)?(.*)")]
        public void GivenAssignmentsCoverMorePeopleThanAssignments(string key1, string key2)
        {
            context.Register()
                   .ForCollection<Assignment>(key1)
                   .ForCollection<Assignment>(key2)
                   .IsTrue((assignments1, assignments2) => assignments1.Sum(x => x.HeadCount) > assignments2.Sum(x => x.HeadCount));
        }

        [Given(@"assignments(?:\s)?(.*) cover as much or more people than assignments(?:\s)?(.*)")]
        public void GivenAssignmentsCoverAsMuchOrMorePeopleThanAssignments(string key1, string key2)
        {
            context.Register()
                   .ForCollection<Assignment>(key1)
                   .ForCollection(c => c.GetToken<Assignment>(key2))
                   .IsTrue((assignments1, assignments2) => assignments1.Sum(x => x.HeadCount) >= assignments2.Sum(x => x.HeadCount));
        }

        [Given(@"assignment(?:\s)?(.*) has one item in insurances(?:\s)?(.*). Do not pay any attention to how dumb it sounds")]
        public void GivenAssignmentHasSomePoliciesSoNotPayAnyAttentionToHowDumbItSounds(string assignmentKey, string insuranceKey)
        {
            context.Register()
                   .For<Assignment>(assignmentKey)
                   .ForCollection<Insurance>(insuranceKey)
                   .IsTrue((assignment, insurances) => insurances.Count() == 1);
        }

        [Given(@"all assignments(?:\s)?(.*) in insurance(?:\s)?(.*) meet following criteria")]
        public void GivenAllAssignmentsInInsuranceMeetFollowingCriteria(string assignmentKey, string insuranceKey)
        {
            context.Register()
                   .For<Insurance>(insuranceKey)
                   .Each<Assignment>(x => x.Assignments, assignmentKey);
        }
    }
}
