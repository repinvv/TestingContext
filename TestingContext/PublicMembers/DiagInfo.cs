namespace TestingContextCore.PublicMembers
{
    using System.Linq.Expressions;
    using static ExpressionToCodeLib.ExpressionToCode;

    public class DiagInfo
    {
        public DiagInfo(string file, int line, string member, Expression filterExpression = null)
        {
            FilterString = filterExpression == null ? null : AnnotatedToCode(filterExpression);
            File = file;
            Line = line;
            Member = member;
        }

        public string FilterString { get; }

        public string File { get; }

        public int Line { get; }

        public string Member { get; }
    }
}
