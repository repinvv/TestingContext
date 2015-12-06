namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public static class InterfaceExtension
    {
        public static IRegister Not(this IRegister register,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            IRegister output = null;
            register.Not(x => output = x, file, line, member);
            return output;
        }

        public static T Value<T>(this IMatcher context, IToken<T> token)
            => context.All(token)
                      .Select(x => x.Value)
                      .FirstOrDefault();

        public static T Value<T>(this IMatcher context, string name)
            => context.All<T>(name)
                      .Select(x => x.Value)
                      .FirstOrDefault();
    }
}
