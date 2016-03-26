namespace UnitTestProject1.Definitions.Common.Then
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContext.Interface;
    using TestingContextCore.PublicMembers;

    [Binding]
    internal class LoggerMustProduceInfo
    {
        private readonly ITestingContext context;
        private string log;

        public LoggerMustProduceInfo(TestingContext context)
        {
            this.context = context;
        }

        [AfterScenarioBlock]
        public void BindLogger()
        {
            if (ScenarioContext.Current.CurrentScenarioBlock != ScenarioBlock.Given)
            {
                return;
            }
            var matcher = context.SetTestMatcher();
            if (matcher.FoundMatch())
            {
                return;
            }

            var f = matcher.GetFailure();
            if (f == null)
            {
                return;
            }

            log = $"entities: {string.Join(", ", f.ForTokens.Select(x => x.ToString()))}:\r\n" + f.Diagnostics;
            Console.Write(log);
            Debug.Write(log);
        }
        [Then(@"resolution logger must produce info for filter, mentioning '(.*)' and '(.*)'")]
        public void ThenResolutionLoggerMustProduceInfoForFilterMentioningAnd(string first, string second)
        {
            Assert.IsTrue(log.Contains(first));
            Assert.IsTrue(log.Contains(second));
        }
    }
}
