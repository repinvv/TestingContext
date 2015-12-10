namespace TestingContextCore.Implementation.Registrations.LoopDetection
{
    using TestingContext.LimitedInterface;

    internal class Reliance
    {
        public IToken Token { get; set; }

        public IToken ReliesOn { get; set; }
    }
}