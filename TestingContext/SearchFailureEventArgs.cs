namespace TestingContextCore
{
    using System.Collections.Generic;

    public class SearchFailureEventArgs
    {
        public IEnumerable<string> Entities { get; set; }

        public string FilterText { get; set; }

        public string FilterKey { get; set; }
    }
}