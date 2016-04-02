namespace UnitTestProject1.NewDefinitions.Companies
{
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContextCore.PublicMembers;
    using TestingContextInterface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyGiven
    {
        private readonly ITestingContext context;

        public CompanyGiven(TestingContext context)
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
