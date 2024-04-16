namespace CostsApi.ProjectServices.Request_Records
{
	public record AddServiceRequest(string ProjectName, string ServiceName, double Cost, string Description);
}
