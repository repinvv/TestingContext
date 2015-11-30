namespace TestingContextCore.PublicMembers
{
    using System;
    using System.Linq.Expressions;
    using global::TestingContext.Interface;
    using static ExpressionToCodeLib.ExpressionToCode;

    public class DiagInfo : IDiagInfo
    {
        private static Func<DiagInfo> diagFactory;

        public static void SetCustomFactory(Func<DiagInfo> factory)
        {
            diagFactory = factory;
        }

        internal static DiagInfo Create(string file, int line, string member, string additionalInfo)
        {
            var diagInfo = diagFactory?.Invoke() ?? new DiagInfo();
            diagInfo.AdditionalInfo = additionalInfo;
            diagInfo.File = file;
            diagInfo.Line = line;
            diagInfo.Member = member;
            return diagInfo;
        }

        internal static DiagInfo Create(string file, int line, string member, Expression filterExpression = null) 
            => Create(file, line, member, filterExpression == null ? null : AnnotatedToCode(filterExpression));

        public string AdditionalInfo { get; internal set; }

        public string File { get; internal set; }

        public int Line { get; internal set; }

        public string Member { get; internal set; }
    }
}
