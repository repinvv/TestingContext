namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;

    public interface IGet
    {
        IEnumerable<IResolutionContext<T1>> Get<T1>(string key = null);
    }
}
