namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;

    public class ProviderCreator<T> : ICreateProvider<T>
    {
        public void Exists<T2>(string key, Func<T, IEnumerable<T2>> srcFunc)
        {
        }

        public void DoesNotExist<T2>(string key, Func<T, IEnumerable<T2>> srcFunc)
        { }

        public void Each<T2>(string key, Func<T, IEnumerable<T2>> srcFunc)
        { }

        public void Satisfies<T2>(string key, Func<T, T2> srcFunc)
        {
            Exists(key, x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            });
        }

        public void DoesNotSatisfy<T2>(string key, Func<T, T2> srcFunc)
        {
            DoesNotExist(key, x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            });
        }
    }
}
