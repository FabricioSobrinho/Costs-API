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

				return projects;
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
				return Results.Ok();
			});

			app.MapPut("projects/:{ProjectName}", async (string ProjectName, UpdateProjectRequest request, AppDbContext context) =>
			{
				var project = await context.Projects.SingleOrDefaultAsync(x => x.ProjectName == ProjectName);

				if  (project == null)
				{
					return Results.NotFound("Projeto não encontrado");
				}

				project.ProjectName = request.ProjectName;
				project.Cost = request.ProjectCosts;
				project.Budget = request.ProjectBudget;
				project.Category = request.Category;

				await context.SaveChangesAsync();

				return Results.Ok(project);
			});
		}
	}
}
