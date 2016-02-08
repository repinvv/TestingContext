namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;

    internal static class NodeClosestParentService
    {
        public static int FindClosestParent(List<INode> chain1, List<INode> chain2)
        {
            var index = 0;
            while (chain1[index].Token == chain2[index].Token)
            {
                index++;
            }

            return index - 1;
        }
    }
}
