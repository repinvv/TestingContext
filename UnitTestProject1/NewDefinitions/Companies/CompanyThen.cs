namespace UnitTestProject1.NewDefinitions.Companies
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers.Exceptions;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyThen
    {
        private readonly ITestingContext context;

        public CompanyThen(ITestingContext context)
        {
            this.context = context;
        }

        [Then(@"companies ""(.*)"" should be found for company(?:\s)?(.*) requirements")]
        public void ThenCompaniesShouldBeFound(string companies, string name)
        {
            var companiesList = companies.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            var foundCompanies = context.GetMatcher().All<Company>(name).Select(x => x.Value.Name).ToList();
            CollectionAssert.AreEquivalent(companiesList, foundCompanies);
        }

        [Then(@"i should get a detailed exception trying to search for company(?:\s)?(.*)")]
        public void ThenIGetADetailedExceptionTryingToSearchForCompany(string name)
        {
            try
            {
                var companies = context.GetMatcher().All<Company>(name);
            }
            catch (DetailedRegistrationException ex)
            {
                context.Storage.Set(ex);
            }
        }
    }
}
