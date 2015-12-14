namespace UnitTestProject1.Definitions.Common
{
    using BoDi;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;

    [Binding]
    public class Register
    {
        private readonly IObjectContainer objectContainer;

        public Register(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void RegisterContext()
        {
            var context = new TestingContext();
            objectContainer.RegisterInstanceAs<ITestingContext>(context);
        }
    }
}
