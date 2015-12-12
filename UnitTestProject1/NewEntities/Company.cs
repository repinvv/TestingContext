namespace UnitTestProject1.NewEntities
{
    using System.Collections.Generic;

    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Department> Departments { get; set; }

        public List<CompanyProperty> CompanyProperty { get; set; }
    }
}
