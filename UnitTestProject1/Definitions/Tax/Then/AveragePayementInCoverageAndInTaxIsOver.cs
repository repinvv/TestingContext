namespace UnitTestProject1.Definitions.Tax.Then
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using TestingContextCore.Interfaces;
    using UnitTestProject1.Entities;

    [Binding]
    public class AveragePayementInCoverageAndInTaxIsOver
    {
        private readonly TestingContext context;

        public AveragePayementInCoverageAndInTaxIsOver(TestingContext context)
        {
            this.context = context;
        }

        [Given(@"average payment per person in coverages(?:\s)?(.*), specified in taxes(?:\s)?(.*) is over (.*)\$")]
        public void GivenAveragePaymentPerPersonInCoveragesBSpecifiedInTaxIsOver(string coverageKey, string taxKey, int average)
        {
            context
                .ForCollection<Coverage>(coverageKey)
                .WithCollection<Tax>(taxKey)
                .Filter((coverages, taxes) => taxes.Sum(x => x.Value.Amount) / coverages.Sum(x => x.Value.HeadCount) > average);
        }
    }
}
