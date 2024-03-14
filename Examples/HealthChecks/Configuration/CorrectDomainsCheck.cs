using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks.Configuration;

[HealthCheck("f80cb939-5715-4344-84bc-e6285b13ba3c", "domains check",
    Description = "Check to see if domain names are correctly set for environment",
    Group = "Site Configuration")]
public class CorrectDomainsCheck : HealthCheck
{
    private readonly IDomainService _domainService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CorrectDomainsCheck(IDomainService domainService, IWebHostEnvironment webHostEnvironment)
    {
        _domainService = domainService;
        _webHostEnvironment = webHostEnvironment;
    }

    public override Task<IEnumerable<HealthCheckStatus>> GetStatus() =>
        Task.FromResult((IEnumerable<HealthCheckStatus>)new[] { NoSpecificDomainCheck() });

    public override HealthCheckStatus ExecuteAction(HealthCheckAction action)
    {
        throw new InvalidOperationException("Action not supported");
    }

    private HealthCheckStatus NoSpecificDomainCheck()
    {
        var domains = new List<string>();
        using HttpClient client = new();
        var allDomains = _domainService.GetAll(false).Select(d => d.DomainName).ToList();


        if (_webHostEnvironment.IsProduction())
        {
            foreach (var domain in allDomains)
            {

                if (domain.ToLower().EndsWith(Constants.DomainName))
                {
                    domains.Add(domain);
                }
            }


        }

        if (domains.Any())
        {
            return new HealthCheckStatus(
                $"The following  domain names needs to be removed {string.Join(",", domains)}")
            {
                ResultType = StatusResultType.Error
            };
        }


        if (!allDomains.Any())
        {
            return new HealthCheckStatus(
                $"No domains are registered for sites")
            {
                ResultType = StatusResultType.Warning
            };
        }


        return
            new HealthCheckStatus("OK")
            {
                ResultType = StatusResultType.Success
            };
    }
}