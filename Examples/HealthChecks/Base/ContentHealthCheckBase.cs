using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Examples.HealthChecks;


public abstract class ContentHealthCheckBase : HealthCheck
{
    private readonly IUmbracoContextFactory _contextFactory;

    public ContentHealthCheckBase(IUmbracoContextFactory contextFactory)
    {
	    _contextFactory = contextFactory;
    }

    public override Task<IEnumerable<HealthCheckStatus>> GetStatus() =>
        Task.FromResult(CheckUrls());

    public override HealthCheckStatus ExecuteAction(HealthCheckAction action)
    {
        switch (action.Alias)
        {
            default:
                throw new InvalidOperationException("Action not supported");
        }
    }

    public IEnumerable<HealthCheckStatus> CheckUrls()
    {
	    var result = new List<HealthCheckStatus>();
	    using var ctx = _contextFactory.EnsureUmbracoContext();
	    
	    var rootNodesWithTemplates = ctx.UmbracoContext.Content?.GetAtRoot().Where(n => n.TemplateId.HasValue).Select(n => n.Url(mode: UrlMode.Absolute));

	    using var client = new HttpClient();
        if (rootNodesWithTemplates != null)
        {
            foreach (var url in rootNodesWithTemplates)
            {
                try
                {
                    var response = Task.Run(async () => await client.GetAsync(url));
                    var responseAsString = Task.Run(async () => await response.Result.Content.ReadAsStringAsync());

                    result.Add(CheckForContent(url, responseAsString.Result));
                }
                catch (Exception e)
                {
                    result.Add(new HealthCheckStatus(e.Message)
                    {
                        ResultType = StatusResultType.Error
                    });
                }
            }
        }

        return result;
    }

    public abstract HealthCheckStatus CheckForContent(string url, string content);


}