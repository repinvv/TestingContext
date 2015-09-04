namespace TestingContextCore.Implementation.Registrations
{
    internal interface ISource
    {
        EntityDefinition EntityDefinition { get; }

        ISource Parent { get; }
    }
}
