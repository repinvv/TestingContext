namespace UnitTestProject1.NewDefinitions
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextInterface;

    [Binding]
    public class ThenException
    {
        private readonly ITestingContext context;

        public ThenException(TestingContext context)
        {
            this.context = context;
        }

        [Then(@"Detailed exception should mention types ""(.*)""")]
        public void ThenSearchingForCompanyShouldResultExceptionMentioningAnd(string types)
        {
            var ex = context.Storage.Get<DetailedRegistrationException>();
            Assert.IsNotNull(ex);
            var typeList = types.Split(',');
            foreach (var typeName in typeList)
            {
                Assert.IsTrue(ex.Message.Contains(typeName));
                Assert.IsTrue(ex.DetailedDiagnostics.Any(x => x.Item1.ToString().Contains(typeName)));
            }
        }
    }
}
