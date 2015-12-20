namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System.Runtime.InteropServices;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Providers;

    internal class ProviderVertex : Vertex<ProviderVertex>
    {
        public IToken Token { get; set; }

        public IProvider Provider { get; set; }
    }
}
