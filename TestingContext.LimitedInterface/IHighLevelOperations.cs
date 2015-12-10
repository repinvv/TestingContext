namespace TestingContext.LimitedInterface
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IHighLevelOperations<TCaller>
    {
        IFilterToken Not(Action<TCaller> action,
         [CallerFilePath] string file = "",
         [CallerLineNumber] int line = 0,
         [CallerMemberName] string member = "");

        IFilterToken Or(Action<TCaller> action,
                Action<TCaller> action2,
                Action<TCaller> action3 = null,
                Action<TCaller> action4 = null,
                Action<TCaller> action5 = null,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "");

        IFilterToken Xor(Action<TCaller> action,
                 Action<TCaller> action2,
                 [CallerFilePath] string file = "",
                 [CallerLineNumber] int line = 0,
                 [CallerMemberName] string member = "");
    }
}
