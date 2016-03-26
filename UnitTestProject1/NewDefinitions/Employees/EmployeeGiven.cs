namespace UnitTestProject1.NewDefinitions.Employees
{
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class EmployeeGiven
    {
        private readonly ITestingContext context;

        public EmployeeGiven(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have empleyees in departments")]
        public void GivenIHaveEmpleyeesInDepartments(Table table)
        {
            List<Employee> employees = table.CreateSet<Employee>().ToList();
            context.Storage.Set(employees);
            var departments = context.Storage.Get<List<Department>>();
            foreach (var empGroup in employees.GroupBy(x => x.DepartmentId))
            {
                var department = departments.First(x => x.Id == empGroup.Key);
                if (department.Employees == null)
                {
                    department.Employees = new List<Employee>();
                }

                department.Employees.AddRange(empGroup);
            }
        }

    }
}
