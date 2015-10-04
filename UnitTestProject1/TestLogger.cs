namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal class TestLogger : IResolutionLog
    {
        public List<string> Logs { get; } = new List<string>();

        public void LogNoItemsResolved(string entity, string filter, string key, bool inverted)
        {
            var log = $"key: {key}\r\nentity: {entity}:\r\ninverter: {inverted}\r\n{filter}\r\n";
            Logs.Add(log);
            Console.Write(log);
        }
    }
}
