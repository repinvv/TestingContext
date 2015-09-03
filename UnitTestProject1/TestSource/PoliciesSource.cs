namespace UnitTestProject1.TestSource
{
    using System;
    using System.Collections.Generic;
    using UnitTestProject1.Entities;

    public static class PoliciesSource
    {
        public static Policy[] Policies =
            {
                new Policy
                    {
                        Id = 1,
                        Name = "noname",
                        Created = new DateTime(2013, 1, 2),
                        Covered =
                            new List<Covered>
                                {
                                    new Covered { Id = 1, Amount = 10 },
                                    new Covered { Id = 2, Amount = 20 },
                                    new Covered { Id = 3, Amount = 30 }
                                }
                    },
                new Policy
                    {
                        Id = 2,
                        Name = "somename",
                        Created = new DateTime(2013, 1, 2),
                        Covered =
                            new List<Covered>
                                {
                                    new Covered { Id = 4, Amount = 30 },
                                    new Covered { Id = 5, Amount = 40 },
                                }
                    },
                new Policy
                    {
                        Id = 3,
                        Name = "nonaem",
                        Created = new DateTime(2013, 1, 2),
                        Covered =
                            new List<Covered>
                                {
                                    new Covered { Id = 6, Amount = 50 },
                                    new Covered { Id = 7, Amount = 40 },
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
