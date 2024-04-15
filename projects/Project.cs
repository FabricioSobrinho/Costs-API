using CostsApi.ProjectServices;
using CostsApi.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CostsApi.Projects
{
	[Index(nameof(ProjectName), IsUnique = true)]
	public class Project
	{
		public Guid Id { get; init; }

		[Display(Name = "Project name")]
		[Required(ErrorMessage = "{0} is a required field")]
		[StringLength(60, MinimumLength = 6, ErrorMessage = "{0} size should be between {2} and {1} chars")]
		public string ProjectName { get; set; }

		[Display(Name = "budget")]
		[Required(ErrorMessage = "{0} is a required field")]
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
