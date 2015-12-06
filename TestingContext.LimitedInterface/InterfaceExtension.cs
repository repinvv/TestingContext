namespace TestingContext.LimitedInterface
{
    using System.Runtime.CompilerServices;

    public static class InterfaceExtension
    {
        public static ITokenRegister Not(this ITokenRegister register,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            ITokenRegister output = null;
            register.Not(x => output = x, file, line, member);
            return output;
        }
    }
}
