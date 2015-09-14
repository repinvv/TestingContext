namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System;

    internal static class ResolutionStrategyFactory
    {
        private static readonly EachResolutionStrategy each;
        private static readonly ExistsResolutionStrategy exists;
        private static readonly DoesNotExistResolutionStrategy doesNotExist;
        private static readonly EmptyResolutionStrategy empty;

        static ResolutionStrategyFactory()
        {
            exists = new ExistsResolutionStrategy();
            doesNotExist = new DoesNotExistResolutionStrategy();
            each = new EachResolutionStrategy();
            empty = new EmptyResolutionStrategy();
        }

        public static IResolutionStrategy Root() => empty;

        public static IResolutionStrategy Exists() => exists;

        public static IResolutionStrategy DoesNotExist() => doesNotExist;

        public static IResolutionStrategy Each() => each;

        public static IResolutionStrategy Count(Func<int, bool> countFunc)
        {
            return new CountForResolutionStrategy(countFunc);
        }
    }
}
