namespace Community.Umbraco.Checklist.Core.Models;

public sealed class StateInformation
{
	public CheckListItemStatus Status { get; set; } = new();

	public string? State { get; set; }

}