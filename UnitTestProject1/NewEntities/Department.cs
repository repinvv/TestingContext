namespace UnitTestProject1.NewEntities
{
    using System.Collections.Generic;

    public class Department
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public DepartmentType Type { get; set; }

        public List<Employee> Employees { get; set; }

        public List<WorkProject> Projects { get; set; } 
    }
}
