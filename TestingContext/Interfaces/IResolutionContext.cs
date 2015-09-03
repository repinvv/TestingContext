namespace TestingContextCore.Interfaces
{
    public interface IResolutionContext<T>
    {
        T Value { get; }
    }
}
