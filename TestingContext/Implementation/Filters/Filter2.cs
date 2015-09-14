namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly Func<T1, T2, bool> filterFunc;
        private readonly Definition[] definitions;

        public Filter2(Definition[] definitions, Func<T1, T2, bool> filterFunc)
        {
            this.definitions = definitions;
            this.filterFunc = filterFunc;
        }

        public Definition[] Definitions => definitions;

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument1 = context.GetValue<T1>(definitions[0]);
            var argument2 = context.GetValue<T2>(definitions[1]);
            return filterFunc(argument1, argument2);
        }

        public void ValidateFilter(ContextStore store)
        { }
    }
}
