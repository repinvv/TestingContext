namespace UnitTestProject1.NewDefinitions.WorkProjects
{
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TestingContext.Interface;
    using UnitTestProject1.NewEntities;

    [Binding]
    public class WorkProjectGiven
    {
        private readonly ITestingContext context;

        public WorkProjectGiven(ITestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have work projects in departments")]
        public void GivenIHaveWorkProjectsInDepartments(Table table)
        {
            List<WorkProject> projects = table.CreateSet<WorkProject>().ToList();
            context.Storage.Set(projects);
            var departments = context.Storage.Get<List<Department>>();
            foreach (var projGroup in projects.GroupBy(x => x.DepartmentId))
            {
                var department = departments.First(x => x.Id == projGroup.Key);
                if (department.Projects == null)
                {
                    department.Projects = new List<WorkProject>();
                }

                department.Projects.AddRange(projGroup);
            }
        }
    }
}
