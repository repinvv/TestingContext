namespace UnitTestProject1.Definitions.Common.Then
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore;

    [Binding]
    internal class LoggerMustProduceInfo
    {
        private readonly TestingContext context;
        private readonly List<string> logs = new List<string>();

        public LoggerMustProduceInfo(TestingContext context)
        {
            this.context = context;
        }

        [BeforeScenario]
        public void BindLogger()
        {
            context.OnSearchFailure += OnSearchFailure;
        }

        public void OnSearchFailure(object sender, SearchFailureEventArgs e)
        {
            var log = $"key: {e.FilterKey}\r\nentities: {string.Join(", ", e.Entities)}:\r\n{e.FilterText}\r\n";
            logs.Add(log);
            Console.Write(log);
            Debug.Write(log);
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
