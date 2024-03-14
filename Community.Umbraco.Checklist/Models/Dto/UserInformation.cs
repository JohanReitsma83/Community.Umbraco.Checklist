namespace Community.Umbraco.Checklist.Models.Dto;

public class UserInformation
{
	public string Name { get; set; } = string.Empty;
	public bool Active { get; set; }

	public string Email { get; set; } = string.Empty;
}