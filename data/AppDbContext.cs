using Microsoft.EntityFrameworkCore;

using CostsApi.Projects;

namespace CostsApi.Data
{
	public class AppDbContext : DbContext
	{
		protected readonly IConfiguration _configuration;

		public AppDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
		}
		public DbSet<Project> Projects { get; set; }
	}
}
