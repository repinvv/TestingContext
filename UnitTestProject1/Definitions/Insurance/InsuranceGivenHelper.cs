namespace UnitTestProject1.Definitions.Insurance
{
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;

    public static class InsuranceGivenHelper
    {
        public static void InsuranceHasMaximumDependents(this IRegister register, string insuranceKey)
        {
            register.For(c => c.GetToken<Insurance>(insuranceKey))
                    .IsTrue(x => x.MaximumDependents > 0);
        }

        public static void InsuranceHasFederalTax(this IRegister register, string insuranceKey)
        {
            var tax = register
                .For(c => c.GetToken<Insurance>(insuranceKey))
                .Exists<Tax>(x => x.Taxes);
            register.For(tax)
                    .IsTrue(x => x.Type == TaxType.Federal);
        }

        public static void InsuranceHasDependentAssignment(this IRegister register, string insuranceKey)
        {
            var assignment = register
                .For(c => c.GetToken<Insurance>(insuranceKey))
                .Exists<Assignment>(x => x.Assignments);
            register.For(assignment)
                    .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
