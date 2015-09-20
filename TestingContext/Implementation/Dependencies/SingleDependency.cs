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

        public SingleDependency(Definition definition, Definition dependency)
        {
            this.definition = definition;
            DependsOn = dependency;
        }

        public TSource GetValue(IResolutionContext context)
        {
            var resolved = context.GetContext(DependsOn) as IResolutionContext<TSource>;
            if (resolved == null)
            {
                throw new ResolutionException($"Could not resolve the value of {DependsOn}, " +
                                              "this most likely means no item meets the specified conditions");
            }

            return resolved.Value;
        }

        public void Validate(ContextStore store)
        {
            if (definition.Equals(DependsOn))
            {
                return; // should not ever get here
            }
            var node = store.GetNode(definition);
            var depend = store.GetNode(DependsOn);
            if (node.IsChildOf(depend))
            {
                return;
            }


        }

        public Definition DependsOn { get; }
    }
}
