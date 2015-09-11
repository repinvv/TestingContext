namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    internal static class ResolutionStrategyFactory
    {
        private static readonly EachResolutionStrategy each;
        private static readonly ExistsResolutionStrategy exists;
        private static readonly DoesNotExistResolutionStrategy doesNotExist;

        static ResolutionStrategyFactory()
        {
            exists = new ExistsResolutionStrategy();
            doesNotExist = new DoesNotExistResolutionStrategy();
            each = new EachResolutionStrategy();

        }

        public static IResolutionStrategy Root() => exists;

        public static IResolutionStrategy Exists() => exists;

        public static IResolutionStrategy DoesNotExist() => doesNotExist;

        public static IResolutionStrategy Each() => each;
    }
}
