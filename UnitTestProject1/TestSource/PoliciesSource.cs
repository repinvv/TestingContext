﻿namespace UnitTestProject1.TestSource
{
    using System;
    using System.Collections.Generic;
    using UnitTestProject1.Entities;
    using static Entities.TaxType;
    using static Entities.AssignmentType;

    public static class PoliciesSource
    {
        public static Insurance[] Insurances =
        {
            new Insurance // should be skipped by tests
            {
                Id = 1,
                Created = new DateTime(2004, 1, 2)
            },
            new Insurance // for @simpleEvaluation1 and @notFoundLogging2
            {
                Id = 2,
                Name = "insurance for @simpleEvaluation1",
                Created = new DateTime(2006, 1, 2),
                Assignments =
                    new List<Assignment>
                    {
                        new Assignment { Id = 1, Type = Employee, HeadCount = 30 }
                    }
            },
            new Insurance // for @simpleEvaluation2
            {
                Id = 3,
                Name = "somename2014",
                Created = new DateTime(2007, 1, 2),
                MaximumDependents = 71,
                Assignments = new List<Assignment>
                            {
                                new Assignment { Id = 2, Type = Employee, HeadCount = 40 },
                                new Assignment { Id = 3, Type = Dependent, HeadCount = 70 }
                            }
            },
            new Insurance // for @notFoundLogging3
            {
                Id = 4,
                Created = new DateTime(2009, 1, 2)
            },
            new Insurance
            {
                Id = 5,
                Name = "non matching insurance for two branch",
                Created = new DateTime(2010, 1, 2),
                Assignments = new List<Assignment>
                            {
                                new Assignment { Id = 4, Type = Employee, HeadCount = 40 },
                                new Assignment { Id = 5, Type = Dependent, HeadCount = 70 }
                            },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 1, Type = Local, Amount = 800 },
                            new Tax { Id = 2, Type = Federal, Amount = 600 }
                        }
            },
            new Insurance
            {
                Id = 6,
                Name = "matching insurance for @twobranch1",
                Created = new DateTime(2010, 1, 2),
                Assignments = new List<Assignment>
                            {
                                new Assignment { Id = 6, Type = Employee, HeadCount = 40 },
                                new Assignment { Id = 7, Type = Dependent, HeadCount = 70 }
                            },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 3, Type = Local, Amount = 600 },
                            new Tax { Id = 4, Type = Federal, Amount = 800 }
                        }
            },
            new Insurance
            {
                Id = 7,
                Name = "matching insurance for @breakOne3",
                Created = new DateTime(2011, 1, 2),
                Assignments = new List<Assignment>
                            {
                                new Assignment { Id = 8, Type = Dependent, HeadCount = 22 }
                            },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 5, Type = Federal, Amount = 65 }
                        }
            },
            new Insurance
            {
                Id = 8,
                Name = "matching insurance for @breakOne2",
                Created = new DateTime(2011, 1, 2),
                Assignments = new List<Assignment>(),
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 6, Type = Federal, Amount = 88 }
                        }
            },
            new Insurance
            {
                Id = 9,
                Name = "matching insurance for @breakOne1",
                Created = new DateTime(2011, 1, 2),
                Assignments = new List<Assignment>
                            {
                                new Assignment { Id = 9, Type = Dependent, HeadCount = 22 }
                            },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 7, Type = Federal, Amount = 88 }
                        }
            }
        };
    }
}
