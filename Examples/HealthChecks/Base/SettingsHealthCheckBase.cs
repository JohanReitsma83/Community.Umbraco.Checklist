using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.HealthChecks.Checks;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks;

public abstract class SettingsHealthCheckBase : AbstractSettingsCheck
{
	private readonly IConfiguration _configuration;


	protected SettingsHealthCheckBase(IConfiguration configuration, ILocalizedTextService textService) : base(textService)
	{
		_configuration = configuration;
	}

	
	

	public override ValueComparisonType ValueComparisonType => Comparison;

	public override string ItemPath => ConfigItemPath;

	public override string CurrentValue => _configuration.GetValue<string>(ItemPath);

	public abstract ValueComparisonType Comparison { get; }





	public abstract string ConfigItemPath { get; }
}