using CostsApi.ProjectServices;
using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public class Project
	{
		public Guid Id { get; init; }
		public string ProjectName { get; set; }
		public double Budget { get; set; }
		public double Cost { get; set; }
		public Category Category { get; set; }
		public List<ProjectService> Services { get; set; }

		public Project(string projectName, double budget, Category category)
		{
			Id = Guid.NewGuid();
			ProjectName = projectName!;
			Budget = budget;
			Cost = 0;
			Category = category;
			Services = new List<ProjectService>();
		}
	}
}
