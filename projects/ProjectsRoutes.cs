using CostsApi.Data;
using CostsApi.ProjectServices;
using Microsoft.EntityFrameworkCore;

namespace CostsApi.Projects
{
	public static class ProjectsRoutes
	{
		public static void AddProjectsRoutes(WebApplication app)
		{
			app.MapGet("projects", async (AppDbContext context, CancellationToken ct) =>
			{
				var projects = await context.Projects.Select(project => new ProjectDto(project.ProjectName, project.Cost, project.Budget, project.Category, project.Services.Select(service => new ProjectServiceDto(service.ServiceName, service.Cost, service.Description)).ToList())).ToListAsync(ct);

				return projects;
			});

			app.MapPost("projects", async (AddProjectRequest request, AppDbContext context, CancellationToken ct) =>
			{
				var newProject = new Project(request.ProjectName, request.Budget, request.Category);

				var projectExists = await context.Projects.AnyAsync((project) => project.ProjectName == newProject.ProjectName, ct);

				if (projectExists)
				{
					return Results.Conflict("Projeto já existe");
				}

				await context.Projects.AddAsync(newProject, ct);
				await context.SaveChangesAsync(ct);

				var returnProject = new ProjectDto(newProject.ProjectName, newProject.Cost, newProject.Budget, newProject.Category, newProject.Services.Select(service => new ProjectServiceDto(service.ServiceName, service.Cost, service.Description)).ToList());
				return Results.Ok(newProject);
			});

			app.MapPut("projects/:{ProjectName}", async (string ProjectName, UpdateProjectRequest request, AppDbContext context, CancellationToken ct) =>
			{
				var project = await context.Projects.SingleOrDefaultAsync(x => x.ProjectName == ProjectName, ct);

				if (project == null)
				{
					return Results.NotFound("Projeto não encontrado");
				}

				project.ProjectName = request.ProjectName;
				project.Cost = request.ProjectCosts;
				project.Budget = request.ProjectBudget;
				project.Category = request.Category;

				await context.SaveChangesAsync(ct);

				var returnProject = new ProjectDto(project.ProjectName, project.Cost, project.Budget, project.Category, project.Services.Select(service => new ProjectServiceDto(service.ServiceName, service.Cost, service.Description)).ToList());
				return Results.Ok(returnProject);
			});

			app.MapDelete("projects/:{ProjectName}", async (string ProjectName, AppDbContext context, CancellationToken ct) =>
			{
				var project = await context.Projects.SingleOrDefaultAsync(x => x.ProjectName == ProjectName, ct);

				if (project == null)
				{
					return Results.NotFound("Projeto não encontrado");
				}

				context.Projects.Remove(project);
				await context.SaveChangesAsync(ct);

				var returnProject = new ProjectDto(project.ProjectName, project.Cost, project.Budget, project.Category, project.Services.Select(service => new ProjectServiceDto(service.ServiceName, service.Cost, service.Description)).ToList());
				return Results.Ok(returnProject);
			});
		}
	}
}
