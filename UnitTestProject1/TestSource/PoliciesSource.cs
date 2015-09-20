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
                new Policy
                    {
                        Id = 1,
                        Name = "somenaem2013",
                        Created = new DateTime(2013, 1, 2),
                        Coverages =
                            new List<Coverage>
                            {
                              new Coverage { Id = 1, Type  = Employee, HeadCount = 30 },
                            }
                    },
                new Policy
                    {
                        Id = 2,
                        Name = "somename2014",
                        Created = new DateTime(2014, 1, 2),
                        Coverages =
                            new List<Coverage>
                                {
                                    new Coverage { Id = 4, Type = Employee, HeadCount = 40 },
                                    new Coverage { Id = 5, Type  = Dependent, HeadCount = 70 },
                                }
                    },
                new Policy
                    {
                        Id = 3,
                        Name = "nonaem",
                        Created = new DateTime(2013, 1, 2),
                        Coverages =
                            new List<Coverage>
                                {
                                    new Coverage { Id = 6, HeadCount = 50 },
                                    new Coverage { Id = 7, HeadCount = 40 },
                                }
                    },
                new Policy
                    {
                        Id = 4,
                        Name = "somenaem",
                        Created = new DateTime(2014, 1, 2)
                    },
                new Policy
                    {
                        Id = 5,
                        Name = "somenaem",
                        Created = new DateTime(2014, 1, 2)
                    },
                new Policy
                    {
                        Id = 6,
                        Name = "somenaem",
                        Created = new DateTime(2014, 1, 2)
                    },
            };
    }
}
