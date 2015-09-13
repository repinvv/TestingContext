namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal static class ResolutionContextExtension
    {
        public static T GetValue<T>(this IResolutionContext context, Definition definition)
        {
            var resolved = context.GetContext(definition) as IResolutionContext<T>;
            if (resolved == null)
            {
                throw new ResolutionException($"Could not resolve the value of {definition}, " +
                                              "this most likely means no item meets the specified conditions");
            }

            return resolved.Value;
        }
    }
}
