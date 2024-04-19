using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public record UpdateProjectRequest(string ProjectName, double Budget, Category Category);
}
