using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Web;

namespace Examples.HealthChecks.Content;

[HealthCheck("4da97b1c-54dd-405e-a678-64e3ff84e45e", "GTM Scripts",
	Description = "GTM Script",
	Group = "Marketing")]
public class GtmContentHealthCheck : ContentHealthCheckBase
{
	public GtmContentHealthCheck(IUmbracoContextFactory contextFactory) : base(contextFactory)
	{
	}

	public override HealthCheckStatus CheckForContent(string url, string content)
	{
		if (content.Contains("https://www.googletagmanager.com/gtm.js"))
		{
			return new HealthCheckStatus("OK")
			{
				ResultType = StatusResultType.Success
			};

		}
		return new HealthCheckStatus($"No gtm script found on url {url}")
		{
			ResultType = StatusResultType.Error
		};


	}
}