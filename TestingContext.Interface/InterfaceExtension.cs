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

        public static void Exists<T1, T2>(this IFor<T1> ifor,
            string name,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(name, ItemFunc(srcFunc), file, line, member);

        public static void DoesNotExist<T1, T2>(this IFor<T1> ifor,
            string name,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(name, ItemFunc(srcFunc), file, line, member);

        private static Func<T1, IEnumerable<T2>> ItemFunc<T1, T2>(Func<T1, T2> srcFunc)
        {
            return x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            };
        }

        public static void Exists<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(name, ItemFunc(srcFunc), file, line, member);

        public static void DoesNotExist<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(name, ItemFunc(srcFunc), file, line, member);

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
