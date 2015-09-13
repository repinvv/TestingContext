namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Interfaces;

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly Func<T1, T2, bool> filterFunc;

        public Filter2(Definition[] definitions, Func<T1, T2, bool> filterFunc)
        {
            Definitions = definitions;
            this.filterFunc = filterFunc;
        }

        public Definition[] Definitions { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument1 = context.GetValue<T1>(Definitions[0]);
            var argument2 = context.GetValue<T2>(Definitions[1]);
            return filterFunc(argument1, argument2);
        }
    }
}
