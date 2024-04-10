using CostsApi.ProjectServices;
using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public class Project
	{
		public Guid Id { get; init; }
		public double Budget { get; set; }
		public double Cost { get; set; }
		public Category Category { get; set; }
		public List<ProjectService> Services { get; set; }

		public Project(double budget, Category category)
		{
			Id = Guid.NewGuid();
			Budget = budget;
			Cost = 0;
			Category = category;
			Services = new List<ProjectService>();
		}
	}
}
