namespace CostsApi.ProjectServices
{
	public class ProjectService
	{
		public Guid Id { get; set; }
		public string ProjectName { get; set; }
        public string ServiceName { get; set; }
		public double Cost { get; set; }
		public string Description { get; set; }

		public ProjectService(string projectName, string serviceName, double cost, string description)
		{
			Id = Guid.NewGuid();
			ProjectName = projectName;
			ServiceName = serviceName;
			Cost = cost;
			Description = description;
		}
	}
}
