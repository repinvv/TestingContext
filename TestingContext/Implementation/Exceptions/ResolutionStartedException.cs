namespace TestingContextCore.Implementation.Exceptions
{
    using System;

    internal class ResolutionStartedException : Exception
    {
        public ResolutionStartedException(string message) : base(message)
        {
        }
    }
}
