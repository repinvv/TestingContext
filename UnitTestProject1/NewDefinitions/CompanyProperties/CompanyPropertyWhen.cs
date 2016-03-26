namespace UnitTestProject1.NewDefinitions.CompanyProperties
{
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyPropertyWhen
    {
        private readonly ITestingContext context;

        public CompanyPropertyWhen(TestingContext context)
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

        [When(@"i specify that company property(?:\s)?(.*) depends on company(?:\s)?(.*) and employee(?:\s)?(.*)")]
        public void WhenITrySpecifyingThatCompanyPropertyDependsOnCompanyAndEmployee(string propKey, string companyKey, string empKey)
        {
            context.For<Company>(companyKey)
                   .For<Employee>(empKey)
                   .Exists(propKey, (company, employee) => company.CompanyProperty);
        }
    }
}
