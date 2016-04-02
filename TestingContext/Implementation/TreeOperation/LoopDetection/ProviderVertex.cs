namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using TestingContextCore.Implementation.Providers;
    using TestingContextLimitedInterface.Tokens;

    internal class ProviderVertex : Vertex<ProviderVertex>
    {
        public IToken Token { get; set; }

        public IProvider Provider { get; set; }
    }
}
