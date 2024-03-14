using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks.Configuration;

[HealthCheck("e27efa88-2a6f-4bc2-9029-090bb1475867", "Smtp Sender ",
    Description = "Check to see if the sender is a customer set email address",
    Group = "Site Configuration")]
public class SmtpSenderHealthCheck : SettingsHealthCheckBase
{
    private readonly IWebHostEnvironment _environment;

    public SmtpSenderHealthCheck(IWebHostEnvironment environment, IConfiguration configuration, ILocalizedTextService textService) : base(configuration, textService)
    {
        _environment = environment;
    }

    public override string ReadMoreLink => string.Empty;

    public override IEnumerable<AcceptableConfiguration> Values
    {
        get
        {
            if (_environment.IsDevelopment())
            {
                new AcceptableConfiguration()
                {
                    IsRecommended = true,
                    Value = Constants.PreferredEmail
                };
            }
            return new AcceptableConfiguration[]
            {
                new AcceptableConfiguration()
                {
                    IsRecommended = true,
                    Value = Constants.PreferredCustomerEmail
                }
            };
        }
    }



    public override ValueComparisonType Comparison => ValueComparisonType.ShouldNotEqual;


    public override string ConfigItemPath => "Umbraco:CMS:Content:Notifications:Email";


}