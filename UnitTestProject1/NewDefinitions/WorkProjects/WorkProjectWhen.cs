namespace UnitTestProject1.NewDefinitions.WorkProjects
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    internal class WorkProjectWhen
    {
        private readonly ITestingContext context;

        public WorkProjectWhen(ITestingContext context)
        {
            this.context = context;
        }

        [When(@"i specify that project(?:\s)?(.*) depends on department(?:\s)?(.*) and company property(?:\s)?(.*)")]
        public void WhenISpecifyThatProjectDependsOnDepartmentAndCompanyProperty(string projKey, string depKey, string propKey)
        {
            context.For<Department>(depKey)
                   .For<CompanyProperty>(propKey)
                   .Exists(projKey, (department, property) => department.Projects);
        }

    }
}
