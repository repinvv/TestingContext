namespace TestingContextCore.PublicMembers
{
    using System;
    using System.Linq.Expressions;
    using static ExpressionToCodeLib.ExpressionToCode;

    public class DiagInfo
    {
        private static Func<DiagInfo> diagFactory;

        public static void SetCustomFactory(Func<DiagInfo> factory)
        {
            diagFactory = factory;
        }

        internal static DiagInfo Create(string file, int line, string member, Expression filterExpression = null)
        {
            var diagInfo = diagFactory == null ? new DiagInfo() : diagFactory();
            diagInfo.FilterString = filterExpression == null ? null : AnnotatedToCode(filterExpression);
            diagInfo.File = file;
            diagInfo.Line = line;
            diagInfo.Member = member;
            return diagInfo;
        }

        public string FilterString { get; internal set; }

        public string File { get; internal set; }

        public int Line { get; internal set; }

        public string Member { get; internal set; }
    }
}
