namespace TestingContextCore.Interfaces.Inversion
{
    using System.Runtime.CompilerServices;

    public interface INameInversion
    {
        void InvertFilter(string name,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void InvertCollectionValidity<T>(string name,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void InvertItemValidity<T>(string name,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
