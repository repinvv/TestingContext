namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal static class FailureReasonExtension
    {
        public static IResolutionContext<T> LoggedFirstOrDefault<T>(this ContextStore store, IEnumerable<IResolutionContext<T>> source)
        {
            if (source == null)
            {
                return null;
            }

            var first = source.FirstOrDefault();
            if (first == null && store.Logging)
            {
                LogResolutionFailure(source as IResolution);
            }

            return first;
        }

        public static IResolutionContext LoggedFirstOrDefault(this ContextStore store, IResolution source)
        {
            if (source == null)
            {
                return null;
            }

            var first = source.FirstOrDefault();
            if (first == null && store.Logging)
            {
                LogResolutionFailure(source);
            }

            return first;
        }

        private static void LogResolutionFailure(IResolution source)
        { }
    }
}
