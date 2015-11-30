namespace TestingContextCore.Implementation.Logging
{
    using TestingContextCore.Implementation.Filters;

    internal class FailureCollect
    {
        private int[] currentWeight;

        public FailureCollect()
        {
            currentWeight = new int[0];
        }

        public IFilter Failure { get; private set; }

        public void ReportFailure(int[] failureWeight, IFilter faiure)
        {
            if (WeightIsBigger(failureWeight))
            {
                currentWeight = failureWeight;
                Failure = faiure;
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
