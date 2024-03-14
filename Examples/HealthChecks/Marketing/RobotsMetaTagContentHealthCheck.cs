using System.Text.RegularExpressions;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Web;

namespace Examples.HealthChecks.Content;

[HealthCheck("1f7a92e7-abb5-48a2-a46d-0b583fcfd2ff", "Robots MetaTags",
	Description = "Robots MetaTags",
	Group = "Marketing")]
public class RobotsMetaTagContentHealthCheck : ContentHealthCheckBase
{
	public RobotsMetaTagContentHealthCheck(IUmbracoContextFactory contextFactory) : base(contextFactory)
	{
	}

	public override HealthCheckStatus CheckForContent(string url, string content)
	{
		var metaRegex = new Regex("\\<meta name=\\\"robots\\\".*?\\>");
		var matches = metaRegex.Matches(content);
		if (matches.Count > 1)
		{
			return new HealthCheckStatus($"Multiple metaTags found with robots in body for url {url}")
			{
				ResultType = StatusResultType.Error
			};
		}

		foreach (Match match in matches)
		{
			if (match.Value.Contains("index,follow"))
			{
				return new HealthCheckStatus("OK")
				{
					ResultType = StatusResultType.Success
				};
			}
		}
		return new HealthCheckStatus($"No Meta tag robots found in body with an index, follow for url {url}")
		{
			ResultType = StatusResultType.Error
		};


	}
}