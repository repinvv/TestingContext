namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using System.Collections.Generic;
    using ResolutionContext;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces;

    internal class SingleDependency<TSource> : IDependency<TSource>
        where TSource : class
    {
        private readonly Definition definition;
        private readonly Definition dependency;

        public SingleDependency(Definition definition, Definition dependency)
        {
            this.definition = definition;
            this.dependency = dependency;
        }

        public TSource GetValue(IResolutionContext context)
        {
            var resolved = context.GetContext(dependency) as IResolutionContext<TSource>;
            if (resolved == null)
            {
                throw new ResolutionException($"Could not resolve the value of {dependency}, " +
                                              "this most likely means no item meets the specified conditions");
            }

            return resolved.Value;
        }

        public void Validate(ContextStore store)
        {
            if (definition.Equals(dependency))
            {
                return;
            }
        }
    }
}
