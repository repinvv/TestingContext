namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Linq;

    internal static class ValidationExtension
    {
        public static void ValidateTree(this ContextStore store)
        {
            foreach (var filter in store.Filters.Values.SelectMany(x => x).Where(x => x.Definitions.Length > 1))
            {

            }
        }
    }
}
