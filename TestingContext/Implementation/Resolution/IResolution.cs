namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;

    internal interface IResolution
    {
        IEnumerable<IResolution> GetChildResolution(EntityDefinition entityDefinition);
    }
}
