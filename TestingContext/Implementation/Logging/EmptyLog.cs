namespace TestingContextCore.Implementation.Logging
{
    using TestingContextCore.Interfaces;

    internal class EmptyLog : IResolutionLog
    {
        public void LogNoItemsResolved(string entity, string filter, string key, bool inverted)
        { }
    }
}
