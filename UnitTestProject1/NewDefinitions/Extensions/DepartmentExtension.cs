namespace UnitTestProject1.NewDefinitions.Extensions
{
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Tokens;
    using UnitTestProject1.NewEntities;

    public static class DepartmentExtension
    {
        public static void DepartmentHasType(this ITokenRegister register,
            IHaveToken<Department> department,
            DepartmentType departmentType)
        {
            register.For(department)
                    .IsTrue(dep => dep.Type == departmentType);
        }

        public static IHaveToken<Employee> DepartmentHasEmployeeOfType(this ITokenRegister register,
            IHaveToken<Department> department,
            EmploymentType employmentType)
        {
            var employee = register.For(department)
                                   .Exists<Employee>(dep => dep.Employees);
            register.For(employee)
                    .IsTrue(emp => emp.EmploymentType == employmentType);
            return employee;
        }

        public static void DepartmentHasProjectWithBudget(this ITokenRegister register,
            IHaveToken<Department> department,
            int budget)
        {
            var project = register.For(department)
                                  .Exists<WorkProject>(dep => dep.Projects);
            register.For(project)
                    .IsTrue(proj => proj.Budget >= budget);
        }
    }
}
