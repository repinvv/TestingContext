namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Func<T1, T2, bool> filterFunc;

        public Filter2(IDependency<T1> dependency1, IDependency<T2> dependency2, Func<T1, T2, bool> filterFunc)
        {
            Definitions = new[] { dependency1.DependsOn, dependency2.DependsOn };
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filterFunc = filterFunc;
        }

        public Definition[] Definitions { get; }

        public bool IsPostFilter => dependency2.IsCollectionDependency && dependency2.DependsOnChild;

        public bool IsCollectionFilter => dependency1.IsCollectionDependency;

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument1 = dependency1.GetValue(context);
            var argument2 = dependency2.GetValue(context);
            return filterFunc(argument1, argument2);
        }
    }
}
