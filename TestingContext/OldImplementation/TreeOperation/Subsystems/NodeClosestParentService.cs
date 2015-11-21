namespace TestingContextCore.OldImplementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Nodes;

    internal static class NodeClosestParentService
    {
        public static int FindClosestParent(List<INode> chain1, List<INode> chain2)
        {
            var index = 0;
            while (chain1[index].Definition == chain2[index].Definition)
            {
                index++;
            }

            return index - 1;
        }
    }
}
