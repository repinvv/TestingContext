namespace TestingContextCore.Interfaces
{
    public interface IChildRegistration<TSource> : IRegistration<TSource>
    {
        IRegistration<TSourceFrom> TakesSourceFrom<TSourceFrom>(string key)
            where TSourceFrom : class;
    }
}
