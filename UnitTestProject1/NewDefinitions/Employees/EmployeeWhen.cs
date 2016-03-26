namespace UnitTestProject1.NewDefinitions.Employees
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.PublicMembers;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class EmployeeWhen
    {
        private readonly ITestingContext context;

        public EmployeeWhen(TestingContext context)
        {
            this.context = context;
        }

        [When(@"I need employee name mentioned in company property(?:\s)?(.*)")]
        public void WhenINeedEmployeeNameMentionedInCompanyProperty(string name)
        {
            var employee = context.Storage.Get<IHaveToken<Employee>>();
            context.For(employee)
                   .For<CompanyProperty>(name)
                   .IsTrue((emp, property) => property.Name.Contains(emp.Name));
        }

        [When(@"i specify that employee(?:\s)?(.*) depends on department(?:\s)?(.*) and project(?:\s)?(.*)")]
        public void WhenISpecifyThatEmployeeDependsOnDepartmentAndProject(string empKey, string depKey, string projKey)
        {
            context.For<Department>(depKey)
                   .For<WorkProject>(projKey)
                   .Exists(empKey, (department, project) => department.Employees);

        }
    }
}
