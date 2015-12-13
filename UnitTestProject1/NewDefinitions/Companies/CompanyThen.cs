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

        [Then(@"searching for company(?:\s)?(.*) should result exception mentioning ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void ThenSearchingForCompanyShouldResultExceptionMentioningAnd(string name, string detail1, string detail2, string detail3)
        {
            DetailedRegistrationException ex = null;
            try
            {
                var companies = context.GetMatcher().All<Company>(name);
            }
            catch (DetailedRegistrationException ex1)
            {
                ex = ex1;
            }

            Assert.IsNotNull(ex);
            CheckException(ex, detail1);
            CheckException(ex, detail2);
            CheckException(ex, detail3);
        }

        private void CheckException(DetailedRegistrationException ex, string detail)
        {
            Assert.IsTrue(ex.Message.Contains(detail));
            Assert.IsTrue(ex.DetailedDiagnostics.Any(x => x.Item1.Type.Name.Contains(detail)));
        }
    }
}
