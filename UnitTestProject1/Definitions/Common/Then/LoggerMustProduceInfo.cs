namespace UnitTestProject1.Definitions.Common.Then
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;
    using TestingContextCore.PublicMembers;

    [Binding]
    internal class LoggerMustProduceInfo
    {
        private readonly TestingContext context;
        private readonly List<string> logs = new List<string>();

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

            var log = $"name: {f.Token.Name}\r\nentities: {string.Join(", ", f.ForTokens.Select(x=>x.ToString()))}:\r\n" +
                      $"{f.DiagInfo.File}, Line: {f.DiagInfo.Line}\r\n" +
                      $"{f.DiagInfo.Member}\r\n" +
                      $"{f.DiagInfo.FilterString}\r\n";
        }
        [Then(@"resolution logger must produce info for filter, mentioning '(.*)' and '(.*)'")]
        public void ThenResolutionLoggerMustProduceInfoForFilterMentioningAnd(string first, string second)
        {
            Assert.AreEqual(1, logs.Count);
            Assert.IsTrue(logs[0].Contains(first));
            Assert.IsTrue(logs[0].Contains(second));
        }
    }
}
