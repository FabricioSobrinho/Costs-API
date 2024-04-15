using CostsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CostsApi.Projects
{
	public static class ProjectsRoutes
	{
		public static void AddProjectsRoutes(WebApplication app)
		{
			app.MapGet("projects", async (AppDbContext context) =>
			{
				var projects = await context.Projects.ToListAsync();
				var projectsDtos = new List<ProjectDto>();

				foreach (var project in projects)
				{
					var projectDto = new ProjectDto(project.ProjectName, project.Cost, project.Budget, project.Category);
					projectsDtos.Add(projectDto);
				}

				return projectsDtos;
			});

			app.MapPost("projects", async (AddProjectRequest request, AppDbContext context) =>
			{
				var newProject = new Project(request.ProjectName, request.Budget, request.Category);

				var projectExists = await context.Projects.AnyAsync((project) => project.ProjectName == newProject.ProjectName);

				if (projectExists)
				{
					return Results.Conflict("Projeto já existe");
				}

				await context.Projects.AddAsync(newProject);
				await context.SaveChangesAsync();

				var returnProject = new ProjectDto(newProject.ProjectName, newProject.Cost, newProject.Budget, newProject.Category);
				return Results.Ok(newProject);
			});

			app.MapPut("projects/:{ProjectName}", async (string ProjectName, UpdateProjectRequest request, AppDbContext context) =>
			{
				var project = await context.Projects.SingleOrDefaultAsync(x => x.ProjectName == ProjectName);

				if (project == null)
				{
					return Results.NotFound("Projeto não encontrado");
				}

				project.ProjectName = request.ProjectName;
				project.Cost = request.ProjectCosts;
				project.Budget = request.ProjectBudget;
				project.Category = request.Category;

				await context.SaveChangesAsync();

				var returnProject = new ProjectDto(project.ProjectName, project.Cost, project.Budget, project.Category);
				return Results.Ok(returnProject);
			});
		}
	}
}
