using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Core.Interfaces;

namespace Community.Umbraco.Checklist.Models;


internal  class CheckListItem : ICheckListItem
{
	public CheckListItem(string name, string uniqueAlias, string category, string description, string trigger)
	{
		Name = name;
		UniqueAlias = uniqueAlias;
		Category = category;
		Description = description;
        Trigger = trigger;
    }


    public string Name { get; }
	public string UniqueAlias { get; }
	public string Category { get; }
	public string Description { get; }

    public CheckListType CheckListType => CheckListType.Manual;
    public string Trigger { get; }
}

