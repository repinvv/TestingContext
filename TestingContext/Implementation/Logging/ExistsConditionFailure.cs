namespace TestingContextCore.Implementation.Logging
{
    internal class CustomFailure : IFailure
    {
        public CustomFailure(string customFailure)
        {
            FailureString = customFailure;
        }

        #region IFailure members
        public string FailureString { get; }

        public string Key => null;

        public bool Inverted => false;
        #endregion
    }
}
