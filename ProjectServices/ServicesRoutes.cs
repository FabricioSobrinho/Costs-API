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
				var services = await context.Services.Where(service => service.ProjectName == ProjectName).
					Select(service => new ProjectServiceDto(service.ServiceName, service.Cost, service.Description)).ToListAsync(ct);

				return Results.Ok(services);
			});

			app.MapPost("services", async (AddServiceRequest request, AppDbContext context, CancellationToken ct) =>
			{
				var newService = new ProjectService(request.ProjectName, request.ServiceName, request.Cost, request.Description);

				var project = await context.Projects.FirstOrDefaultAsync(project => project.ProjectName == request.ProjectName, ct);
				if (project == null)
				{
					return Results.NotFound("Projeto não existe");
				}

				var serviceExists = await context.Services.AnyAsync(service => service.ServiceName == request.ServiceName, ct);
				if (serviceExists)
				{
					return Results.BadRequest("Serviço já existe");
				}

				project.Services.Add(newService);
				project.Cost += newService.Cost;

				if (project.Cost > project.Budget)
				{
					return Results.BadRequest("Custo do serviço não deve ser maior que o orçamento do projeto");
				}

				await context.Services.AddAsync(newService, ct);
				await context.SaveChangesAsync(ct);

				return Results.Ok(new ProjectServiceDto(newService.ServiceName, newService.Cost, newService.Description));
			});

			app.MapDelete("services/:{ServiceName}", async (string ServiceName, AppDbContext context, CancellationToken ct) =>
			{
				var service = await context.Services.FirstOrDefaultAsync(service => service.ServiceName == ServiceName, ct);
				
				if (service == null)
				{
					return Results.NotFound("Serviço não existe");
				}

				var project = await context.Projects.FirstOrDefaultAsync(project => project.Id == service.ProjectId, ct);
				if (project == null)
				{
					return Results.NotFound("Projeto não existe");
				}
				project.Cost -= service.Cost;

				context.Services.Remove(service); 
				await context.SaveChangesAsync(ct);

				return Results.Ok("Serviço deletado");
			});
		}
	}
}
