using CostsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CostsApi.ProjectServices
{
	public class ServicesRoutes
	{
		public static void AddServicesRoutes(WebApplication app)
		{
			app.MapGet("services/:{ProjectName}", async (string ProjectName, AppDbContext context, CancellationToken ct) =>
			{
				var services = await context.Services.Where(service => service.ProjectName == ProjectName).ToListAsync();

				return Results.Ok(services);
			});
		}
	}
}
