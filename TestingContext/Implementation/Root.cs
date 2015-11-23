namespace TestingContextCore.Implementation
{
    // dummy root
    internal class Root
    {
        private Root() { }

        public static Root Instance { get; } = new Root();
    }
}
