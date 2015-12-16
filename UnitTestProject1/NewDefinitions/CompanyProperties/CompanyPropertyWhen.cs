namespace UnitTestProject1.NewDefinitions.CompanyProperties
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyPropertyWhen
    {
        private readonly ITestingContext context;

        public CompanyPropertyWhen(ITestingContext context)
        {
            this.context = context;
        }

        [When(@"i need company(?:\s)?(.*) to have ""(.*)"" property(?:\s)?(.*)")]
        public void WhenINeedCompanyToHaveProperty(string companyName, PropertyType type, string propertyName)
        {
            context.For<Company>(companyName)
                   .Exists(propertyName, company => company.CompanyProperty);
            context.For<CompanyProperty>(propertyName)
                   .IsTrue(property => property.Type == type);
        }
    }
}
