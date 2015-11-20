namespace UnitTestProject1.Definitions.Insurance
{
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;

    internal static class InsuranceGivenHelper
    {
        public static void CreateHighLevelCondition<T>(this IForContext<T> forContext, string insuranceKey)
        {
            forContext.For<Insurance>(insuranceKey)
                .IsTrue(x=>x.MaximumDependents > 0);
            forContext.For<Insurance>(insuranceKey)
                    .Exists<Tax>(x => x.Taxes);
            forContext.For<Tax>()
                .IsTrue(x=>x.Type == TaxType.Federal);
            forContext.For<Insurance>(insuranceKey)
                    .Exists<Assignment>(x => x.Assignments);
            forContext.For<Assignment>()
                .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
