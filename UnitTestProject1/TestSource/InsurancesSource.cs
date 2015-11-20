namespace UnitTestProject1.TestSource
{
    using System;
    using System.Collections.Generic;
    using UnitTestProject1.Entities;
    using static Entities.TaxType;
    using static Entities.AssignmentType;

    public static class InsurancesSource
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
            },
            new Insurance
            {
                Id = 10,
                Name = "non-matching insurance for tree reordering",
                Created = new DateTime(2012, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 10, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 3) }
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 8, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 4) }
                        }
            },
            new Insurance
            {
                Id = 11,
                Name = "matching insurance for @treeReordering1",
                Created = new DateTime(2012, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 11, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 3) },
                                  new Assignment { Id = 12, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 5) }

                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 9, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 4) },
                            new Tax { Id = 10, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 5) }
                        }
            },
            new Insurance
            {
                Id = 12,
                Name = "matching insurance for @treeReordering4",
                Created = new DateTime(2012, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 16, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 3) },
                                  new Assignment { Id = 17, Type = Employee, HeadCount = 10, Created = new DateTime(2012, 2, 5) },
                                  new Assignment { Id = 18, Type = Employee, HeadCount = 30, Created = new DateTime(2012, 2, 6) },
                                  new Assignment { Id = 19, Type = Employee, HeadCount = 60, Created = new DateTime(2012, 2, 7) }

                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 11, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 4) },
                            new Tax { Id = 12, Type = Federal, Amount = 20, Created = new DateTime(2012, 2, 5) },
                            new Tax { Id = 13, Type = Federal, Amount = 30, Created = new DateTime(2012, 2, 6) },
                            new Tax { Id = 14, Type = Federal, Amount = 50, Created = new DateTime(2012, 2, 7) }
                        }
            },
            new Insurance
            {
                Id = 13,
                Name = "non-matching insurance for tree reordering 2",
                Created = new DateTime(2013, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 13, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 6) }
                              },
            },
            new Insurance
            {
                Id = 14,
                Name = "matching insurance for @treeReordering2",
                Created = new DateTime(2013, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 14, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 6) },
                                  new Assignment { Id = 15, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 5) }
                              },
            },
            new Insurance
            {
                Id = 15,
                Name = "insurance for @treeReordering3 first",
                Created = new DateTime(2013, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 20, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 6) },
                                  new Assignment { Id = 21, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 5) },
                                  new Assignment { Id = 22, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 5) }
                              },
            },
            new Insurance
            {
                Id = 16,
                Name = "insurance for @treeReordering3 second",
                Created = new DateTime(2014, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 23, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 6) },
                                  new Assignment { Id = 24, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 5) },
                                  new Assignment { Id = 25, Type = Employee, HeadCount = 1, Created = new DateTime(2012, 2, 6) }
                              },
            },
            new Insurance
            {
                Id = 17,
                Name = "non-matching insurance for tree reordering 5",
                Created = new DateTime(2015, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 26, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 3) },
                                  new Assignment { Id = 27, Type = Dependent, HeadCount = 20, Created = new DateTime(2012, 2, 5) },
                                  new Assignment { Id = 28, Type = Dependent, HeadCount = 30, Created = new DateTime(2012, 2, 6) }

                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 15, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 4) },
                            new Tax { Id = 16, Type = Federal, Amount = 100, Created = new DateTime(2012, 2, 5) },
                            new Tax { Id = 17, Type = Federal, Amount = 200, Created = new DateTime(2012, 2, 6) }
                        }
            },
            new Insurance
            {
                Id = 18,
                Name = "matching insurance for @treeReordering5",
                Created = new DateTime(2015, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 29, Type = Dependent, HeadCount = 1, Created = new DateTime(2012, 2, 3) },
                                  new Assignment { Id = 30, Type = Dependent, HeadCount = 20, Created = new DateTime(2012, 2, 5) },
                                  new Assignment { Id = 31, Type = Dependent, HeadCount = 30, Created = new DateTime(2012, 2, 6) }

                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 18, Type = Federal, Amount = 2, Created = new DateTime(2012, 2, 4) },
                            new Tax { Id = 19, Type = Federal, Amount = 300, Created = new DateTime(2012, 2, 5) },
                            new Tax { Id = 20, Type = Federal, Amount = 400, Created = new DateTime(2012, 2, 6) }
                        }
            },
            new Insurance
            {
                Id = 19,
                Name = "matching insurance for @NoFilterLimitation1 and @NonEqualFilter1",
                Created = new DateTime(2015, 1, 2),
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 32, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                                  new Assignment { Id = 33, Type = Dependent, HeadCount = 2900, Created = new DateTime(2012, 2, 5) },
                              }
            },
            new Insurance
            {
                Id = 20,
                Name = "non matching insurance for @ORgroup",
                Created = new DateTime(2016, 1, 2),
                MaximumDependents = 0,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 34, Type = Employee, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 21, Type = Local, Amount = 1 }

                        }
            },
            new Insurance
            {
                Id = 21,
                Name = "matching insurance for @ORgroup1",
                Created = new DateTime(2016, 1, 2),
                MaximumDependents = 1,
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 22, Type = Local, Amount = 1 }
                        }
            },
            new Insurance
            {
                Id = 22,
                Name = "matching insurance for @ORgroup2",
                Created = new DateTime(2016, 1, 2),
                MaximumDependents = 0,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 36, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
            },
            new Insurance
            {
                Id = 23,
                Name = "non matching insurance for @ORgroup3",
                Created = new DateTime(2016, 1, 2),
                MaximumDependents = 0,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 37, Type = Employee, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 24, Type = Federal, Amount = 1 }
                        }
            },
            new Insurance
            {
                Id = 24,
                Name = "non matching insurance for @ORgroup4",
                Created = new DateTime(2016, 1, 2),
                MaximumDependents = 10,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 38, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 25, Type = Federal, Amount = 1 }

                        }
            },
            new Insurance
            {
                Id = 25,
                Name = "non matching insurance for @NOTgroup",
                Created = new DateTime(2017, 1, 2),
                MaximumDependents = 10,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 39, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 26, Type = Federal, Amount = 1 }
                        }
            },
            new Insurance
            {
                Id = 26,
                Name = "matching insurance for @NOTgroup1",
                Created = new DateTime(2017, 1, 2),
                MaximumDependents = 0,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 40, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 27, Type = Federal, Amount = 1 }
                        }
            },
            new Insurance
            {
                Id = 27,
                Name = "matching insurance for @NOTgroup2",
                Created = new DateTime(2017, 1, 2),
                MaximumDependents = 10,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 41, Type = Employee, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 28, Type = Federal, Amount = 1 }
                        }
            },
            new Insurance
            {
                Id = 28,
                Name = "matching insurance for @NOTgroup3",
                Created = new DateTime(2017, 1, 2),
                MaximumDependents = 10,
                Assignments = new List<Assignment>
                              {
                                  new Assignment { Id = 42, Type = Dependent, HeadCount = 1500, Created = new DateTime(2012, 2, 3) },
                              },
            },
            new Insurance
            {
                Id = 29,
                Name = "matching insurance for @NOTgroup4",
                Created = new DateTime(2017, 1, 2),
                MaximumDependents = 0,
                Taxes = new List<Tax>
                        {
                            new Tax { Id = 30, Type = Local, Amount = 1 }
                        }
            },
        };
    }
}
