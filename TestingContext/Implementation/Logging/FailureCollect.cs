namespace TestingContextCore.Implementation.Logging
{
    using TestingContextCore.Implementation.Registrations;

    internal class FailureCollect
    {
        private readonly RegistrationStore store;
        private int[] currentWeight;
        private IFailure currentFailure;
        private Definition currentDefinition;

        public FailureCollect(RegistrationStore store)
        {
            this.store = store;
            currentWeight = new int[0];
        }

        public void LogFailure()
        {
            store.SearchFailure(currentDefinition.ToString(), currentFailure.FilterString, currentFailure.Key, currentFailure.Inverted);
        }

        public void ReportFailure(int[] failureWeight, IFailure faiure, Definition definition)
        {
            if (WeightIsBigger(failureWeight))
            {
                currentWeight = failureWeight;
                currentFailure = faiure;
                currentDefinition = definition;
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
