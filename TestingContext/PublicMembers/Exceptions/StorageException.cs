namespace TestingContextCore.PublicMembers.Exceptions
{
    using System;

    public class StorageException : Exception
    {
        public StorageException(string message) : base(message) { }
    }
}
