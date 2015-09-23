namespace UnitTestProject1.TestSource
{
    using System;
    using System.Collections.Generic;
    using UnitTestProject1.Entities;
    using static UnitTestProject1.Entities.CoverageType;

    public static class PoliciesSource
    {
        public static Policy[] Policies =
        {
            new Policy // should be skipped by tests
            {
                Id = 1,
                Created = new DateTime(2015, 1, 2)
            },
            new Policy // for @test1
            {
                Id = 2,
                Name = "somenaem2013",
                Created = new DateTime(2013, 1, 2),
                Coverages =
                    new List<Coverage>
                    {
                        new Coverage { Id = 1, Type = Employee, HeadCount = 30 },
                    }
            },
            new Policy // for @test2
            {
                Id = 3,
                Name = "somename2014",
                Created = new DateTime(2014, 1, 2),
                Coverages =
                    new List<Coverage>
                    {
                        new Coverage { Id = 2, Type = Employee, HeadCount = 40 },
                        new Coverage { Id = 3, Type = Dependent, HeadCount = 70 },
                    }
            }
        };
    }
}
