namespace UnitTestProject1.Definitions.Assignment
{
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Tokens;
    using UnitTestProject1.Entities;

    internal static class AssignmentGivenHelper
    {
        public static void AssignmentHasMorePeopleThanAssignments
            (this ITokenRegister reg,
             IHaveToken<Assignment> assignmentToken,
             IHaveToken<Assignment> assignmentsToken)
        {
            reg.For(assignmentToken)
               .ForCollection(assignmentsToken)
               .IsTrue((assignment, assignments) => assignment.HeadCount > assignments.Sum(y => y.HeadCount));
        }
    }
}
