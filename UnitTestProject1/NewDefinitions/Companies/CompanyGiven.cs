namespace UnitTestProject1.NewDefinitions.Companies
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyGiven
    {
        private readonly ITestingContext context;

        public CompanyGiven(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have companies")]
        public void GivenIHaveCompanies(Table table)
        {
            context.Storage.Set(table.CreateSet<Company>().ToList());
        }
    }
}
