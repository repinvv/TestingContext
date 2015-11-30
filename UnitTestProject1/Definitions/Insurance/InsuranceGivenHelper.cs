namespace UnitTestProject1.Definitions.Insurance
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using UnitTestProject1.Entities;

    public static class InsuranceGivenHelper
    {
        public static void InsuranceHasMaximumDependents(this ITokenRegister register, IHaveToken<Insurance> insurance)
        {
            register.For(insurance)
                    .IsTrue(x => x.MaximumDependents > 0);
        }

        public static void InsuranceHasFederalTax(this IRegister register, IHaveToken<Insurance> insurance)
        {
            var taxToken = register
                .For(insurance)
                .Exists<Tax>(x => x.Taxes);
            register.For(taxToken)
                    .IsTrue(x => x.Type == TaxType.Federal);
        }

        public static void InsuranceHasDependentAssignment(this IRegister register, IHaveToken<Insurance> insurance)
        {
            var assignmentToken = register
                .For(insurance)
                .Exists(x => x.Assignments);
            register.For(assignmentToken)
                    .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
