namespace TestingContext.LimitedInterface
{
    using System;
    using System.Linq;

    public static class InterfaceExtensions
    {
        public static Action<T>[] Actions<T>(Action<T> action,
            Action<T> action2,
            Action<T> action3,
            Action<T> action4,
            Action<T> action5)
        {
            return new[] { action, action2, action3, action4, action5 }.Where(x => x != null).ToArray();
        }
    }
}
