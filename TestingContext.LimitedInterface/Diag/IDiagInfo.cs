namespace TestingContext.LimitedInterface.Diag
{
    public interface IDiagInfo
    {
        string AdditionalInfo { get; }

        string File { get;}

        int Line { get; }

        string Member { get; }
    }
}
