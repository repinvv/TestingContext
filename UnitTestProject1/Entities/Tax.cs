namespace UnitTestProject1.Entities
{
    using System;

    public class Tax
    {
        public int Id { get; set; }

        public TaxType Type { get; set; }

        public int Amount { get; set; }

        public DateTime Created { get; set; }
    }
}
