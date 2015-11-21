namespace TestingContextCore.OldImplementation.Logging
{
    using System.Linq;

    internal static class ArrayExtension
    {
        public static int[] Add(this int[] array, params int[] numbers)
        {
            return array.Concat(numbers).ToArray();
        }
    }
}
