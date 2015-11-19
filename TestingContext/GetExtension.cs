namespace TestingContextCore
{
    using System.Linq;
    using TestingContextCore.Interfaces;

    public static class GetExtension
    {
        public static T Value<T>(this IGet iget, string key = null)
        {
            return iget.Get<T>(key).Select(x => x.Value).FirstOrDefault();
        }
    }
}
