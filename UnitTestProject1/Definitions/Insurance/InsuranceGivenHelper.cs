namespace UnitTestProject1.Definitions.Insurance
{
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;

    internal static class InsuranceGivenHelper
    {
        public static void CreateHighLevelCondition(this IRegister register, string insuranceKey)
        {
            register.For<Insurance>(insuranceKey)
                .IsTrue(x=>x.MaximumDependents > 0);
            register.For<Insurance>(insuranceKey)
                    .Exists<Tax>(x => x.Taxes);
            register.For<Tax>()
                .IsTrue(x=>x.Type == TaxType.Federal);
            register.For<Insurance>(insuranceKey)
                    .Exists<Assignment>(x => x.Assignments);
            register.For<Assignment>()
                .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
