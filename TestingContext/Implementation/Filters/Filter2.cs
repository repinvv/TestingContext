namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Filter2<T1, T2> : IFilter
        where T1 : class
        where T2 : class
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Func<T1, T2, bool> filterFunc;
        private readonly Definition[] definitions;

        public Filter2(IDependency<T1> dependency1, IDependency<T2> dependency2, Func<T1, T2, bool> filterFunc)
        {
            definitions = new[] { dependency1.DependsOn, dependency2.DependsOn };
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filterFunc = filterFunc;
        }

        public Definition[] Definitions => definitions;

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument1 = dependency1.GetValue(context);
            var argument2 = dependency2.GetValue(context);
            return filterFunc(argument1, argument2);
        }
    }
}
