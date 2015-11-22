namespace UnitTestProject1.Definitions.Insurance
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore.Interfaces;
    using TestingContextCore.PublicMembers;
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

        [Then(@"insurances(?:\s)?(.*) must have names containing '(.*)'")]
        public void ThenInsurancesMustHaveNames(string key, string names)
        {
            var list = names.Split(',').Distinct().ToList();
            var result = context.All<Insurance>(key);
            Assert.IsTrue(list.All(name => result.Count(insurance => insurance.Value.Name.Contains(name)) == 1));
            Assert.AreEqual(list.Count, result.Count());
        }
    }
}
