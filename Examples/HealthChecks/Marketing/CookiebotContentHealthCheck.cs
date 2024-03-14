using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Web;

namespace Examples.HealthChecks.Content;

[HealthCheck("1f7a92e7-abb5-48a2-a46d-0b583fcfd2fe", "Cookiebot Scripts",
	Description = "Cookiebot Script",
	Group = "Marketing")]
public class CookiebotContentHealthCheck : ContentHealthCheckBase
{
	public CookiebotContentHealthCheck(IUmbracoContextFactory contextFactory) : base(contextFactory)
	{
	}

	public override HealthCheckStatus CheckForContent(string url, string content)
	{
		if (content.Contains("https://consent.cookiebot.com/"))
		{
			return new HealthCheckStatus("OK")
			{
				ResultType = StatusResultType.Success
			};

		}
		return new HealthCheckStatus($"No Cookiebot script found on url {url}")
		{
			ResultType = StatusResultType.Error
		};


	}
}