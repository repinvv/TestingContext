﻿namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class FilterGroupRegistration : IFilterRegistration
    {
        private readonly Func<IFilterGroup, IFilterGroup> groupConstructor;

        public FilterGroupRegistration(IFilterToken groupToken, Func<IFilterGroup, IFilterGroup> groupConstructor)
        {
            GroupToken = groupToken;
            this.groupConstructor = groupConstructor;
        }

        public IFilterToken GroupToken { get; }

        public List<IFilterRegistration> FilterRegistrations { get; } = new List<IFilterRegistration>();
        
        public IFilter GetFilter(IFilterGroup group)
        {
            var result = groupConstructor(group);
            result.Filters = FilterRegistrations.Select(x => x.GetFilter(result)).ToList();
            return result;
        }
    }
}
