using CostsApi.Entities.Enums;

namespace CostsApi.Projects
{
	public record ProjectDto(string ProjectName, double Cost, double Budget, Category Category);
}
