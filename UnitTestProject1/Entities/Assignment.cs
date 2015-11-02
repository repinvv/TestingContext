namespace UnitTestProject1.Entities
{
    using System;

    public class Assignment
    {
        public int Id { get; set; }

        public int HeadCount { get; set; }

        public AssignmentType Type { get; set; }

        public DateTime Created { get; set; }
    }
}
