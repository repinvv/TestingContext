namespace TestingContextCore.Implementation.ContextStorage
{
    using TestingContextCore.Implementation.Filters;

    internal static class RegistrationExtension
    {
        public static void RegisterFilter(this ContextStore store, IFilter filter)
        {
            store.Filters.Add(filter);
        }
    }
}
