using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Community.Umbraco.Checklist.Extensions;

public static class ConfigurationExtensions
{
	public static TConfig? ConfigurationJson<TConfig>(this IConfiguration configuration, string key)
	{
		var keyValue = GetJson();

		return JsonSerializer.Deserialize<TConfig>(keyValue) ?? default;

		string GetJson()
		{
			if (typeof(TConfig).IsArray)
			{
				var dictArray = configuration
					.GetSection(key)
					.Get<Dictionary<string, object>[]>();

				return JsonSerializer.Serialize(dictArray);
			}

			var dict = configuration
				.GetSection(key)
				.Get<Dictionary<string, object>>();
			return JsonSerializer.Serialize(dict);
		}
	}
}