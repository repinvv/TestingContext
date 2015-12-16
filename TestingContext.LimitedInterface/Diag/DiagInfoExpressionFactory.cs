namespace TestingContext.LimitedInterface.Expressional
{
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContext.LimitedInterface.Diag;

    public static class DiagInfoExpressionFactory
    {
        public static IDiagInfo CreateDiag(string file, int line, string member, Expression filterExpression)
            => DiagInfo.Create(file, line, member, ExpressionToCode.AnnotatedToCode(filterExpression));
    }
}
