using Community.Umbraco.Checklist.Core.Interfaces;
using Community.Umbraco.Checklist.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Models.Notifications;

public class StatusChangeForChecklistItemNotifition : INotification
{
	public DateTime? TimeCompleted { get; set; }

	public CheckListItemStatus Status { get; }
	public string? State { get; }

	public ICheckListItem CheckListItem { get; set; }
        

	public StatusChangeForChecklistItemNotifition(DateTime? timeCompleted, ICheckListItem checklistItem, CheckListItemStatus status, string? state = null)
	{
		CheckListItem = checklistItem;
		TimeCompleted = timeCompleted;
		Status = status;
		State = state;
	}
}