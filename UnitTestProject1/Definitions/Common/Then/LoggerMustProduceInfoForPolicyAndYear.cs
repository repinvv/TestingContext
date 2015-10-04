namespace UnitTestProject1.Definitions.Common.Then
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;

    [Binding]
    internal class LoggerMustProduceInfoForPolicyAndYear
    {
        private readonly TestLogger logger;

        public LoggerMustProduceInfoForPolicyAndYear(TestLogger logger)
        {
            this.logger = logger;
        }

        [Then(@"resolution logger must produce info for filter, mentioning '(.*)' and '(.*)'")]
        public void ThenResolutionLoggerMustProduceInfoForFilterMentioningAnd(string first, string second)
        {
            Assert.AreEqual(1, logger.Logs.Count);
            Assert.IsTrue(logger.Logs[0].Contains(first));
            Assert.IsTrue(logger.Logs[0].Contains(second));
        }
    }
}
