namespace UnitTestProject1.NewDefinitions.Employees
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Tokens;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class EmployeeWhen
    {
        private readonly ITestingContext context;

        public EmployeeWhen(ITestingContext context)
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
    }
}
