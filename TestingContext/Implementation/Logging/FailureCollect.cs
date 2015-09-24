using TestingContextCore.Implementation.ContextStorage;

namespace TestingContextCore.Implementation.Logging
{
    using TestingContextCore.Implementation.Filters;

    internal class FailureCollect
    {
        private ContextStore store;
        private int[] currentWeight;
        private IFailure currentFailure;

        public FailureCollect(ContextStore store)
        {
            this.store = store;
            currentWeight = new int[0];
        }

        public void LogFailure(Definition definition)
        {
            store.Log.LogNoItemsResolved(definition.ToString(), currentFailure.FailureString);
        }

        public void ReportFailure(int[] failureWeight, IFailure faiure)
        {
            if (WeightIsBigger(failureWeight))
            {
                currentWeight = failureWeight;
                currentFailure = faiure;
            }
        }

        public bool CanCascade(int[] cascadeWeight)
        {
            int i = 0;
            while (i < cascadeWeight.Length && i < currentWeight.Length)
            {
                if (cascadeWeight[i] == currentWeight[i])
                {
                    i++;
                    continue;
                }

                return cascadeWeight[i] > currentWeight[i];
            }

            return true;
        }

        private bool WeightIsBigger(int[] failureWeight)
        {
            int i = 0;
            while (true)
            {
                if (i >= failureWeight.Length)
                {
                    return false;
                }

                if (i >= currentWeight.Length)
                {
                    return true;
                }

                if (failureWeight[i] == currentWeight[i])
                {
                    i++;
                    continue;
                }

                return failureWeight[i] > currentWeight[i];
            }
        }
    }
}
