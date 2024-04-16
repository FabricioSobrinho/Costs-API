using CostsApi.Data;
using CostsApi.ProjectServices.Request_Records;
using Microsoft.EntityFrameworkCore;

namespace CostsApi.ProjectServices
{
	public class ServicesRoutes
	{
		public static void AddServicesRoutes(WebApplication app)
		{
			app.MapGet("services/:{ProjectName}", async (string ProjectName, AppDbContext context, CancellationToken ct) =>
			{
				var services = await context.Services.Where(service => service.ProjectName == ProjectName).ToListAsync(ct);

				return Results.Ok(services);
			});

			app.MapPost("services", async (AddServiceRequest request, AppDbContext context, CancellationToken ct) =>
			{
				var newService = new ProjectService(request.ProjectName, request.ServiceName, request.Cost, request.Description);

				var project = await context.Projects.FirstOrDefaultAsync(project => project.ProjectName ==  request.ProjectName, ct);
				
				if (project == null)
				{
					return Results.NotFound("Projeto não existe");
				}
				
				project.Services.Add(newService);

				await context.Services.AddAsync(newService, ct);
				await context.SaveChangesAsync(ct);

				return Results.Ok(newService);
			});
		}
	}
}
