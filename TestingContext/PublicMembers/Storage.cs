namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.PublicMembers.Exceptions;

    public class Storage
    {
        private readonly Dictionary<Definition, object> store = new Dictionary<Definition, object>();

        public void Set<T>(T value, string key = null)
        {
            store.Add(Definition.Define<T>(key), value);
        }

        public T Get<T>(string key = null)
        {
            object value;
            var definition = Definition.Define<T>(key);
            if (!store.TryGetValue(definition, out value))
            {
                throw new StorageException($"Value for {definition} is not present in the storage.");
            }

            if (!(value is T))
            {
                throw new StorageException($"Value, stored for {definition} has wrong type.");
            }

            return (T)value;
        }
    }
}
