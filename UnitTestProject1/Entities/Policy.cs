namespace UnitTestProject1.Entities
{
    using System;
    using System.Collections.Generic;

    public class Policy
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int MaximumDependents { get; set; }

        public DateTime Created { get; set; }

        public List<Coverage> Coverages { get; set; } 

        public List<Tax> Taxes { get; set; }
    }
}
