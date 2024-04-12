using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public record AddProjectRequest(string ProjectName, double Budget, Category Category);
}
