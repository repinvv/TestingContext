namespace TestingContextCore.PublicMembers
{
    using System;
    using System.Linq.Expressions;
    using global::TestingContext.Interface;
    using static ExpressionToCodeLib.ExpressionToCode;

    public class DiagInfo : IDiagInfo
    {
        private static Func<string, int, string, string, IDiagInfo> diagFactory;

        public static void SetCustomFactory(Func<string, int, string, string, IDiagInfo> factory)
        {
            diagFactory = factory;
        }

        internal static IDiagInfo Create(string file, int line, string member, string additionalInfo)
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

        internal static IDiagInfo Create(string file, int line, string member, Expression filterExpression = null) 
            => Create(file, line, member, filterExpression == null ? null : AnnotatedToCode(filterExpression));

        public string AdditionalInfo { get; internal set; }

        public string File { get; internal set; }

        public int Line { get; internal set; }

        public string Member { get; internal set; }

        public override string ToString()
        {
            return $"File: {File}, line: {Line}{Environment.NewLine}" +
                   $"Member: {Member}, Additional info: {AdditionalInfo}";
        }
    }
}
