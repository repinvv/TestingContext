namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextLimitedInterface.Tokens;

    internal class FilterVertex : Vertex<FilterVertex>
    {
        public IToken Token { get; set; }

        public IFilter Filter { get; set; }

        public IToken Dependency { get; set; }
    }
}
