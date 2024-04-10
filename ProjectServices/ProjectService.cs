namespace CostsApi.ProjectServices
{
	public class ProjectService
	{
		public Guid Id { get; set; }
        public string ServiceName { get; set; }
		public double Cost { get; set; }
		public string Description { get; set; }

		public ProjectService(string serviceName, double cost, string description)
		{
			Id = Guid.NewGuid();
			ServiceName = serviceName;
			Cost = cost;
			Description = description;
		}
	}
}
