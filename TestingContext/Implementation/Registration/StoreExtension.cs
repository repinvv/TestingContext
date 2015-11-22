namespace TestingContextCore.Implementation.Registration
{
    using TestingContextCore.Implementation.Filters;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, IFilter filter, IFilterGroup group)
        {
            store.PreReg();
            if (group != null)
            {
                group.Filters.Add(filter);
                return;
            }

            store.Filters.Add(filter);
        }

        private static void PreReg(this TokenStore store)
        {
            store.Tree = null;
        }
    }
}
