namespace UnitTestProject1.Definitions.Insurance
{
    using TestingContextCore.Interfaces.Register;
    using UnitTestProject1.Entities;

    public static class InsuranceGivenHelper
    {
        public static void InsuranceHasMaximumDependents(this IRegister register, string insuranceKey)
        {
            register.For<Insurance>(insuranceKey)
                    .IsTrue(x => x.MaximumDependents > 0);
        }

        public static void InsuranceHasFederalTax(this IRegister register, string insuranceKey)
        {
            var taxToken = register
                .For<Insurance>(insuranceKey)
                .Exists<Tax>(x => x.Taxes);
            register.For(taxToken)
                    .IsTrue(x => x.Type == TaxType.Federal);
        }

        public static void InsuranceHasDependentAssignment(this IRegister register, string insuranceKey)
        {
            var assignmentToken = register
                .For<Insurance>(insuranceKey)
                .Exists<Assignment>(x => x.Assignments);
            register.For(assignmentToken)
                    .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
