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
                .Declare<Tax>(x => x.Taxes)
                .Exists();
            register.For(taxToken)
                    .IsTrue(x => x.Type == TaxType.Federal);
        }

        public static void InsuranceHasDependentAssignment(this IRegister register, IHaveToken<Insurance> insurance)
        {
            var assignmentToken = register
                .For(insurance)
                .Declare(x => x.Assignments)
                .Exists();
            register.For(assignmentToken)
                    .IsTrue(x => x.Type == AssignmentType.Dependent);
        }
    }
}
