﻿namespace UnitTestProject1.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class PolicyMustHaveId
    {
        private readonly TestingContext testingContext;

        public PolicyMustHaveId(TestingContext testingContext)
        {
            this.testingContext = testingContext;
        }

        [Then(@"policy(?:\s)?(.*) must have id (.*)")]
        public void ThenPolicyMustHaveId(string key, int id)
        {
            Assert.AreEqual(id, testingContext.Value<Policy>(key).Id);
        }
    }
}
