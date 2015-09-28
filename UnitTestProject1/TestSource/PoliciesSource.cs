namespace UnitTestProject1.TestSource
{
    using System;
    using System.Collections.Generic;
    using UnitTestProject1.Entities;
    using static UnitTestProject1.Entities.TaxType;
    using static UnitTestProject1.Entities.CoverageType;

    public static class PoliciesSource
    {
        public static Policy[] Policies =
        {
            new Policy // should be skipped by tests
            {
                Id = 1,
                Created = new DateTime(2004, 1, 2)
            },
            new Policy // for @simpleEvaluation1 and @notFoundLogging2
            {
                Id = 2,
                Name = "policy for @simpleEvaluation1",
                Created = new DateTime(2006, 1, 2),
                Coverages =
                    new List<Coverage>
                    {
                        new Coverage { Id = 1, Type = Employee, HeadCount = 30 },
                    }
            },
            new Policy // for @simpleEvaluation2
            {
                Id = 3,
                Name = "somename2014",
                Created = new DateTime(2007, 1, 2),
                Coverages = new List<Coverage>
                            {
                                new Coverage { Id = 2, Type = Employee, HeadCount = 40 },
                                new Coverage { Id = 3, Type = Dependent, HeadCount = 70 },
                            }
            },
            new Policy // for @notFoundLogging3
            {
                Id = 4,
                Created = new DateTime(2009, 1, 2)
            },
            new Policy
            {
                Id = 5,
                Name = "non matching policy for two branch",
                Created = new DateTime(2010, 1, 2),
                Coverages = new List<Coverage>
                            {
                                new Coverage { Id = 4, Type = Employee, HeadCount = 40 },
                                new Coverage { Id = 5, Type = Dependent, HeadCount = 70 },
                            },
                Taxes = new List<Tax>()
                        {
                            new Tax { Id = 1, Type = Local, Amount = 800 },
                            new Tax { Id = 2, Type = Federal, Amount = 600 }
                        }
            },
            new Policy
            {
                Id = 6,
                Name = "matching policy for @twobranch1",
                Created = new DateTime(2010, 1, 2),
                Coverages = new List<Coverage>
                            {
                                new Coverage { Id = 4, Type = Employee, HeadCount = 40 },
                                new Coverage { Id = 5, Type = Dependent, HeadCount = 70 },
                            },
                Taxes = new List<Tax>()
                        {
                            new Tax { Id = 3, Type = Local, Amount = 600 },
                            new Tax { Id = 4, Type = Federal, Amount = 800 }
                        }
            }
        };
    }
}
