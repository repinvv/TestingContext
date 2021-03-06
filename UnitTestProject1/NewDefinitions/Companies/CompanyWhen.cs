﻿namespace UnitTestProject1.NewDefinitions.Companies
{
    using System.Collections.Generic;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;
    using TestingContextInterface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class CompanyWhen
    {
        private readonly ITestingContext context;

        public CompanyWhen(TestingContext context)
        {
            this.context = context;
        }

        [When(@"i need a company(?:\s)?(.*)")]
        public void WhenISearchInCompanies(string name)
        {
            context.Exists<Company>(name, () => context.Storage.Get<List<Company>>(null));
        }
    }
}
