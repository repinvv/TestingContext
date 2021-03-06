﻿namespace UnitTestProject1.NewDefinitions.Extensions
{
    using TestingContextLimitedInterface;
    using TestingContextLimitedInterface.Tokens;
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
                                   .Exists(dep => dep.Employees);
            register.For(employee)
                    .IsTrue(emp => emp.EmploymentType == employmentType);
            return employee;
        }

        public static void DepartmentHasProjectWithBudget(this ITokenRegister register,
            IHaveToken<Department> department,
            int budget)
        {
            var project = register.For(department)
                                  .Exists(dep => dep.Projects);
            register.For(project)
                    .IsTrue(proj => proj.Budget >= budget);
        }
    }
}
