using System.Text.Json.Serialization;

namespace Community.Umbraco.Checklist.Models.Dto;

public class CheckListItem
{
    public CheckListItem()
    {
        UserInformation = new UserInformation();
    }

	
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("uniqueAlias")]
	public string UniqueAlias { get; set; } = string.Empty;


    [JsonPropertyName("category")] 
    public string Category { get; set; } = string.Empty;

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("description")]
	public string? Description { get; set; }
    
	[JsonPropertyName("lastExecutedBy")]
	public string? LastExecutedBy { get; set; }

	[JsonPropertyName("lastRun")]
	public DateTime? LastRun { get; set; }


	[JsonPropertyName("lastStatus")]
	public long LastStatus { get; set; }

	[JsonPropertyName("messages")]
	public string? Messages { get; set; }

	[JsonPropertyName("runType")]
	public int RunType { get; set; }
    
	[JsonPropertyName("userInfo")]
	public UserInformation UserInformation {
		get;
		set;
	}

}