namespace TestingContextCore
{
    using System.Linq;
    using TestingContextCore.Interfaces;
    using TestingContextCore.NewInterfaces;

    public static class ContextExtension
    {
        public static T Value<T>(this IGet iget, string key = null)
        {
            return iget.Get<T>(key).Select(x => x.Value).FirstOrDefault();
        }

        public static IRegister<T> Not<T>(this IRegister<T> register)
        {
            IRegister<T> output = null;
            register.Not(x => output = x);
            return output;
        }
    }
}
