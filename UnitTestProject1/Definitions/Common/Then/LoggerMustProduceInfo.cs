namespace UnitTestProject1.Definitions.Common.Then
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    internal class LoggerMustProduceInfo
    {
        private readonly TestingContext context;
        private string log;

        public LoggerMustProduceInfo(TestingContext context)
        {
            this.context = context;
        }

        [BeforeScenarioBlock]
        public void BindLogger()
        {
            if (ScenarioContext.Current.CurrentScenarioBlock != ScenarioBlock.Then)
            {
                return;
            }

            if (context.FoundMatch())
            {
                return;
            }

            var f = context.GetFailure();

            log = $"entities: {string.Join(", ", f.ForTokens.Select(x => x.ToString()))}:\r\n" +
                  $"{f.DiagInfo.File}, Line: {f.DiagInfo.Line}\r\n" +
                  $"{f.DiagInfo.Member}\r\n" +
                  $"{f.DiagInfo.AdditionalInfo}\r\n";
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
