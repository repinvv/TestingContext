namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal class TestLogger : IResolutionLog
    {
        public List<string> Logs { get; } = new List<string>();

        public void LogNoItemsResolved(string entity, string filter)
        {
            var log = $"{entity}:\r\n{filter}";
            Logs.Add(log);
            Console.Write(log);
        }
    }
}
