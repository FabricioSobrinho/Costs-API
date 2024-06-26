using CostsApi.Data;
using CostsApi.Projects;
using CostsApi.ProjectServices;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "*";


builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  builder =>
					  {
						  builder.WithOrigins("*")
								 .AllowAnyHeader()
								 .AllowAnyMethod();
					  });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

ProjectsRoutes.AddProjectsRoutes(app);
ServicesRoutes.AddServicesRoutes(app);

app.Run();

