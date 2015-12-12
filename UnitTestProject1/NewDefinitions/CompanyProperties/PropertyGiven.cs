namespace UnitTestProject1.NewDefinitions.CompanyProperties
{
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class PropertyGiven
    {
        private readonly ITestingContext context;

        public PropertyGiven(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have companies property")]
        public void GivenIHaveCompaniesProperty(Table table)
        {
            List<CompanyProperty> properties = table.CreateSet<CompanyProperty>().ToList();
            context.Storage.Set(properties);
            var companies = context.Storage.Get<List<Company>>();
            foreach (var propGroup in properties.GroupBy(x => x.CompanyId))
            {
                var company = companies.First(x => x.Id == propGroup.Key);
                if (company.CompanyProperty == null)
                {
                    company.CompanyProperty = new List<CompanyProperty>();
                }

                company.CompanyProperty.AddRange(propGroup);
            }
        }
    }
}
