namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class FilterGroupRegistration : IFilterRegistration
    {
        private readonly Func<IFilterGroup, int, IFilterGroup> groupConstructor;

        public FilterGroupRegistration(Func<IFilterGroup, int, IFilterGroup> groupConstructor)
        {
            this.groupConstructor = groupConstructor;
        }

        public List<IFilterRegistration> FilterRegistrations { get; } = new List<IFilterRegistration>();

        public int Id { private get; set; }

        public IFilter GetFilter(IFilterGroup group)
        {
            var result = groupConstructor(group, Id);
            result.Filters.AddRange(FilterRegistrations.Select(x => x.GetFilter(result)));
            return result;
        }
    }
}
