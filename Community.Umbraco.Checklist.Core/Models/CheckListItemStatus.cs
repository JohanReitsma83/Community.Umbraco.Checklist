using Community.Umbraco.Checklist.Core.Enums;

namespace Community.Umbraco.Checklist.Core.Models;

public sealed class CheckListItemStatus
{
	public CheckListItemStatusType StatusType { get; set; }


	public IList<string> Messages { get; set; } = new List<string>();
}