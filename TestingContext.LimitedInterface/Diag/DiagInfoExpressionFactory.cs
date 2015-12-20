namespace TestingContext.LimitedInterface.Diag
{
    using System.Linq.Expressions;
    using ExpressionToCodeLib;

    public static class DiagInfoExpressionFactory
    {
        public static IDiagInfo CreateDiag(string file, int line, string member, Expression filterExpression)
            => DiagInfo.Create(file, line, member, ExpressionToCode.AnnotatedToCode(filterExpression));
    }
}
