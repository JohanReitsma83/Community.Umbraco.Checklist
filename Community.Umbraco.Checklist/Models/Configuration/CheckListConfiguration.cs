using System.Text.Json.Serialization;

namespace Community.Umbraco.Checklist.Models.Configuration;

public class CheckListConfiguration
{
	[JsonPropertyName("file")]
	public string? File { get; set; }

	public class ChecklistConfigEntryItem
	{
        [JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

        [JsonPropertyName("uniqueAlias")]
		public string UniqueAlias { get; set; } = string.Empty;

		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

        [JsonPropertyName("category")]
		public string Category { get; set; } = string.Empty;

        [JsonPropertyName("trigger")]
        public string Trigger { get; set; } = string.Empty;
    }
}