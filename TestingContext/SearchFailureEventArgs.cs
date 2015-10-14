namespace TestingContextCore
{
    public class SearchFailureEventArgs
    {
        public string Entity { get; set; }

        public string FilterText { get; set; }

        public string FilterKey { get; set; }

        public bool Inverted { get; set; }
    }
}