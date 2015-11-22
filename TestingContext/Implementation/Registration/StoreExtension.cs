namespace TestingContextCore.Implementation.Registration
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Interfaces.Tokens;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, IFilter filter, IFilterGroup group)
        {
            store.PreRegister();
            if (group != null)
            {
                group.Filters.Add(filter);
                return;
            }

            store.Filters.Add(filter);
        }

        public static void RegisterProvider(this TokenStore store, IProvider provider, IToken token)
        {
            store.PreRegister();
            store.Providers.Add(token, provider);
        }

        private static void PreRegister(this TokenStore store)
        {
            store.Tree = null;
        }
    }
}
