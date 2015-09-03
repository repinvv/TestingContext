namespace TestingContextCore.Implementation
{
    using TestingContextCore.Interfaces;

    public class ResolutionContext<T> : IResolutionContext<T>
    {
        public T Value
        {
            get
            {
                return default(T);
            }
        }
    }
}
