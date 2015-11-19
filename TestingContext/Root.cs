namespace TestingContextCore
{
    // dummy root to avoid root register duplication
    public class Root
    {
        private Root() { }
        internal static Root Instance { get; } = new Root();
    }
}
