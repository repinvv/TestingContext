namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Linq;

    internal static class ValidationExtension
    {
        public static void Validate(this ContextStore store)
        {
            foreach (var dependency in store.Dependencies)
            {
                dependency.Validate(store);
            }
        }

        public static Definition GetClosestParent(this ContextStore store, Definition node, Definition dependsOn)
        {
            return node;
        }

        public static void ValidateDependency(this ContextStore store, Definition node, Definition dependsOn, Definition closestParent)
        {
        }
    }
}
