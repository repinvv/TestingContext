namespace TestingContext.LimitedInterface.Diag
{
    using System;
    using System.Runtime.CompilerServices;

    public class DiagInfo : IDiagInfo
    {
        private static Func<string, int, string, string, IDiagInfo> diagFactory;

        public static void SetCustomFactory(Func<string, int, string, string, IDiagInfo> factory)
        {
            diagFactory = factory;
        }

        public static IDiagInfo Create([CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "",
            string additionalInfo = null)
        {
            return diagFactory?.Invoke(file, line, member, additionalInfo)
                   ?? new DiagInfo
                      {
                          AdditionalInfo = additionalInfo,
                          File = file,
                          Line = line,
                          Member = member
                      };
        }

        public string AdditionalInfo { get; internal set; }

        public string File { get; internal set; }

        public int Line { get; internal set; }

        public string Member { get; internal set; }

        public override string ToString()
        {
            return $"File: {File}, line: {Line}{Environment.NewLine}" +
                   $"Member: {Member},{Environment.NewLine} Additional info:" +
                   $"{Environment.NewLine} {AdditionalInfo}";
        }
    }
}
