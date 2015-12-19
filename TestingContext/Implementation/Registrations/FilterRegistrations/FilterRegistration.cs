namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using System;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class FilterRegistration : IFilterRegistration
    {
        private readonly Func<IFilterGroup, int, IFilter> filterConstructor;

        public FilterRegistration(Func<IFilterGroup, int, IFilter> filterConstructor)
        {
            this.filterConstructor = filterConstructor;
        }

        public int Id { private get; set; }

        public IFilter GetFilter(IFilterGroup group) => filterConstructor(group, Id);
    }
}
