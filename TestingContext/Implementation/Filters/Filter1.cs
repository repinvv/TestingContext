namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Filter1<T1> : IFilter
    {
        private readonly Func<T1, bool> filterFunc;

        public Filter1(Definition definition, Func<T1, bool> filterFunc)
        {
            this.filterFunc = filterFunc;
            Definitions = new []{ definition};
        }

        public Definition[] Definitions { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument = context.GetValue<T1>(Definitions[0]);
            return filterFunc(argument);
        }
    }
}
