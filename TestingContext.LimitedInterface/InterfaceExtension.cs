namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public static IHaveToken<T2> Exists<T1, T2>(this IForToken<T1> ifor,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(ItemFunc(srcFunc), file, line, member);

        public static IHaveToken<T2> DoesNotExist<T1, T2>(this IForToken<T1> ifor,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(ItemFunc(srcFunc), file, line, member);

        private static Func<T1, IEnumerable<T2>> ItemFunc<T1, T2>(Func<T1, T2> srcFunc)
        {
            return x =>
            {
                var item = srcFunc(x);
                return item == null ? new T2[0] : new[] { item };
            };
        }

        public static IHaveToken<T3> Exists<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(ItemFunc(srcFunc), file, line, member);

        public static IHaveToken<T3> DoesNotExist<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(ItemFunc(srcFunc), file, line, member);

        private static Func<T1, T2, IEnumerable<T3>> ItemFunc<T1, T2, T3>(Func<T1, T2, T3> srcFunc)
        {
            return (x, y) =>
            {
                var item = srcFunc(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            };
        }
    }
}
