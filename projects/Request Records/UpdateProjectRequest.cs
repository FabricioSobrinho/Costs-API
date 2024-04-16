using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public record UpdateProjectRequest(string ProjectName, double ProjectCosts, double ProjectBudget, Category Category);
}
