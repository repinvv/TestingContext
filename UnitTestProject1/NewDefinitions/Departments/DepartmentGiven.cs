namespace UnitTestProject1.NewDefinitions.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class DepartmentGiven
    {
        private readonly ITestingContext context;

        public DepartmentGiven(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have departments in companies")]
        public void GivenIHaveDepartmentsInCompanies(Table table)
        {
            List<Department> departments = table.CreateSet<Department>().ToList();
            context.Storage.Set(departments);
            var companies = context.Storage.Get<List<Company>>();
            foreach (var depGroup in departments.GroupBy(x => x.CompanyId))
            {
                var company = companies.First(x => x.Id == depGroup.Key);
                if (company.Departments == null)
                {
                    company.Departments = new List<Department>();
                }

                company.Departments.AddRange(depGroup);
            }
        }
    }
}
