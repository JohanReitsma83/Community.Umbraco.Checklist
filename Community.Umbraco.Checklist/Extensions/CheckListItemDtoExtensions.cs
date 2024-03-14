using Community.Umbraco.Checklist.Models;
using Community.Umbraco.Checklist.Models.Dto;
using CheckListItem = Community.Umbraco.Checklist.Models.Dto.CheckListItem;

namespace Community.Umbraco.Checklist.Extensions;

public static class CheckListItemDtoExtensions
{
	public static CheckListItem EntryToDto(this CheckListEntry entry, Func<string, UserInformation> getUserInfo)
	{
		return new CheckListItem()
		{
			Id = entry.Id,
			Name = entry.Name,
			Category = entry.Category,
			Description = entry.Description,
			LastExecutedBy = entry.LastExecutedBy,
			LastRun = entry.LastRun,
			LastStatus = entry._status,
			Messages = entry.Messages,
			RunType = entry._runType,
			UniqueAlias = entry.UniqueAlias,
			UserInformation = getUserInfo(entry.LastExecutedBy ?? string.Empty)
		};
	}
}