namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore;

    internal class TestLogger
    {
        public List<string> Logs { get; } = new List<string>();

        public void OnSearchFailure(object sender, SearchFailureEventArgs e)
        {
            var log = $"key: {e.FilterKey}\r\nentity: {e.Entity}:\r\ninverter: {e.Inverted}\r\n{e.FilterText}\r\n";
            Logs.Add(log);
            Console.Write(log);
        }
    }
}
