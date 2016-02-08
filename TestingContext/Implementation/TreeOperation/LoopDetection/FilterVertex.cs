namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;

    internal class FilterVertex : Vertex<FilterVertex>
    {
        public IToken Token { get; set; }

        public IFilter Filter { get; set; }

        public IToken Dependency { get; set; }
    }
}
