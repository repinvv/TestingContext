namespace UnitTestProject1.Definitions.Insurance
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using UnitTestProject1.Entities;

    [Binding]
    public class InsuranceThen
    {
        private readonly TestingContext context;

        public InsuranceThen(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"insurance(?:\s)?(.*) must exist")]
        public void ThenInsuranceAMustExist(string key)
        {
            Assert.IsNotNull(context.Value<Insurance>(key));
        }

        [Then(@"insurance(?:\s)?(.*) must have id (.*)")]
        public void ThenInsuranceMustHaveId(string key, int id)
        {
            Assert.AreEqual(id, context.Value<Insurance>(key).Id);
        }

        [Then(@"insurance(?:\s)?(.*) name must contain '(.*)'")]
        public void ThenInsuranceNameMustContain(string key, string namepart)
        {
            var insurance = context.Value<Insurance>(key);
            Assert.IsTrue(insurance.Name.Contains(namepart));
        }
    }
}
