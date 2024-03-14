using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Web;

namespace Examples.HealthChecks.Content;

[HealthCheck("8b4b754b-558c-4ece-83de-bd8b92819ba9", "Sharpspring Scripts",
	Description = "Sharpspring Script",
	Group = "Marketing")]
public class SharpspringContentHealthCheck : ContentHealthCheckBase
{
	public SharpspringContentHealthCheck(IUmbracoContextFactory contextFactory) : base(contextFactory)
	{
	}

	public override HealthCheckStatus CheckForContent(string url, string content)
	{
		if (content.Contains("marketingautomation.services/client/"))
		{
			return new HealthCheckStatus("OK")
			{
				ResultType = StatusResultType.Success
			};

		}
		return new HealthCheckStatus($"No Sharpspring script found on url {url}")
		{
			ResultType = StatusResultType.Error
		};


	}
}