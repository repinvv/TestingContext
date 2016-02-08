namespace UnitTestProject1.NewDefinitions.Departments
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Tokens;
    using UnitTestProject1.NewEntities;
    using UnitTestProject1.NewDefinitions.Extensions;

    [Binding]
    public class DepartmentWhen
    {
        private readonly ITestingContext context;

        public DepartmentWhen(ITestingContext context)
        {
            this.context = context;
        }

        [When(@"i need company(?:\s)?(.*) to have a department(?:\s)?(.*)")]
        public void WhenINeedCompanyToHaveADepartment(string companyName, string departmentName)
        {
            context.For<Company>(companyName)
                   .Exists(departmentName, company => company.Departments);
        }

        [When(@"i need department(?:\s)?(.*) to either be ""(.*)"" type or have ""(.*)"" employee or have project with budjet (.*) or more")]
        public void WhenINeedDepartmentToEitherBeTypeOrHaveEmployeeOrHaveProjectWithBudjetOrMore(string departmentName, 
            DepartmentType departmentType, 
            EmploymentType employeeType,
            int amount)
        {
            IHaveToken<Employee> employee = null;
            var department = context.GetToken<Department>(departmentName);
            context.Either(x => x.DepartmentHasType(department, departmentType),
                       x => employee = x.DepartmentHasEmployeeOfType(department, employeeType),
                       x => x.DepartmentHasProjectWithBudget(department, amount));
            context.Storage.Set(employee);
        }


        [When(@"i need department(?:\s)?(.*) to NOT have all three, ""(.*)"" type, ""(.*)"" employee, project budjet (.*) or more")]
        public void WhenINeedDepartmentToNOTHaveAllThreeTypeEmployeeProjectBudjetOrMore(string departmentName,
            DepartmentType departmentType,
            EmploymentType employeeType,
            int amount)
        {
            IHaveToken<Employee> employee = null;
            var department = context.GetToken<Department>(departmentName);
            context.Not(x =>
            {
                x.DepartmentHasType(department, departmentType);
                employee = x.DepartmentHasEmployeeOfType(department, employeeType);
                x.DepartmentHasProjectWithBudget(department, amount);
            });
            context.Storage.Set(employee);
        }

        [When(@"for company property(?:\s)?(.*) i need department(?:\s)?(.*) to NOT have all three, ""(.*)"" type, ""(.*)"" employee, project budjet (.*) or more")]
        public void WhenForCompanyPropertyINeedDepartmentToNotHaveAllThreeTypeEmployeeProjectBudjetOrMore(
            string propertyName,
            string departmentName,
            DepartmentType departmentType,
            EmploymentType employeeType,
            int amount)
        {
            IHaveToken<Employee> employee = null;
            var department = context.GetToken<Department>(departmentName);
            context
                .For<CompanyProperty>(propertyName)
                .Not(x =>
                {
                    x.DepartmentHasType(department, departmentType);
                    employee = x.DepartmentHasEmployeeOfType(department, employeeType);
                    x.DepartmentHasProjectWithBudget(department, amount);
                });
            context.Storage.Set(employee);
        }

        [When(@"i need department(?:\s)?(.*) to have higher headcount that departments(?:\s)?(.*)")]
        public void WhenINeedDepartmentToHaveHigherHeadcountThatDepartments(string depKey1, string depKey2)
        {
            context
                .For<Department>(depKey1)
                .ForCollection<Department>(depKey2)
                .IsTrue((department, departments) => department.HeadCount > departments.Sum(x => x.HeadCount));
        }

    }
}
