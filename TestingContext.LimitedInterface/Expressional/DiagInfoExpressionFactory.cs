namespace TestingContext.LimitedInterface.Expressional
{
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContext.LimitedInterface.Diag;

    public class DiagInfoExpressionFactory
    {
        internal static IDiagInfo Create(string file, int line, string member, Expression filterExpression = null)
            => DiagInfo.Create(file, line, member, filterExpression == null ? null : ExpressionToCode.AnnotatedToCode(filterExpression));
    }
}
