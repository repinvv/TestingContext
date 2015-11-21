namespace TestingContextCore
{
    using System.Linq;
    using TestingContextCore.Interfaces;

    public static class ContextExtension
    {
        public static T Value<T>(this ITestingContext context, string key = null)
        {
            return iget.Get<T>(key).Select(x => x.Value).FirstOrDefault();
        }
    }
}
