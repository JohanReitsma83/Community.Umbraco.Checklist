using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks.Configuration;

[HealthCheck("b9b751cf-0d7a-46a8-b729-662669e1fa58", "RecaptchaV3PrivateKey",
    Description = "Check to see if the recaptch V3 key is set",
    Group = "Site Configuration")]
public class RecaptchaV3PrivateKeyHealthCheck : SettingsHealthCheckBase
{
    public RecaptchaV3PrivateKeyHealthCheck(IConfiguration configuration, ILocalizedTextService textService) : base(configuration, textService)
    {
    }

    public override string ReadMoreLink { get; }

    public override IEnumerable<AcceptableConfiguration> Values => new AcceptableConfiguration[]
    {
        new AcceptableConfiguration()
        {
            IsRecommended = true,
            Value = "XXXXXXXXXXX_XXXXXXXXXX"
        },
        new AcceptableConfiguration()
        {
            Value = null
        }
    };

    public override ValueComparisonType Comparison => ValueComparisonType.ShouldNotEqual;
    public override string ConfigItemPath => "Umbraco:Forms:FieldTypes:Recaptcha3:PrivateKey";
}