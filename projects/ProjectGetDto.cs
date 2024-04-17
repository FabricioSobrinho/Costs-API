using CostsApi.Entities.Enums;
using CostsApi.ProjectServices;

namespace CostsApi.Projects
{
	public record ProjectGetDto(Guid Id, string ProjectName, double Cost, double Budget, Category Category, List<ProjectServiceDto> Services);
}
