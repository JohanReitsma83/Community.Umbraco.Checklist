using System.Net;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks.Configuration
{
    [HealthCheck("3A482719-3D90-4BC1-B9F8-910CD9CF5B36", "404",
        Description = "Check to see if there is a correct 404",
        Group = "SEO")]
    public class Status404HealthCheck : HealthCheck
    {
        private readonly IDomainService _domainService;

        public Status404HealthCheck(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public override Task<IEnumerable<HealthCheckStatus>> GetStatus() =>
            Task.FromResult((IEnumerable<HealthCheckStatus>)new[] { CheckFor404() });

        public override HealthCheckStatus ExecuteAction(HealthCheckAction action)
        {
            switch (action.Alias)
            {
                default:
                    throw new InvalidOperationException("Action not supported");
            }
        }

        private HealthCheckStatus CheckFor404()
        {
            var domainsWith404 = new List<string>();
            using HttpClient client = new();
            var allDomains = _domainService.GetAll(false).Select(d => d.DomainName).ToList();

            foreach (var domain in allDomains)
            {
                var url = $"{domain}/{Guid.NewGuid()}";
                var result = Task.Run(async () => await client.GetAsync(url));
                if (result.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    domainsWith404.Add(domain);
                }
            }

            if (domainsWith404.Any())
            {
                return new HealthCheckStatus(
                    $"The following domain names doesn't have a 404 enabled {string.Join(",", domainsWith404)}")
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
}
