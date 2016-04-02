namespace TestingContextInterface
{
    public interface IStorage
    {
        void Set<T>(T value, string key = null);

        T Get<T>(string key = null);
    }
}
