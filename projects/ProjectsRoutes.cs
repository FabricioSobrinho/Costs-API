namespace CostsApi.Projects
{
	public static class ProjectsRoutes
	{
		public static void AddProjectsRoutes(WebApplication app)
		{
			app.MapGet("projects", () => new Project(21000, Entities.Enums.Category.Development));
		}
	}
}
