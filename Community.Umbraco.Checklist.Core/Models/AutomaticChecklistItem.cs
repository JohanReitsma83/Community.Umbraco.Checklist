using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Core.Interfaces;

namespace Community.Umbraco.Checklist.Core.Models;

public class AutomaticChecklistItem : ICheckListItem
{
    public AutomaticChecklistItem(string name, string uniqueAlias, string category, string description)
    {
        Name = name;
		UniqueAlias = uniqueAlias;
        Category = category;
        Description = description;
        

    }

    public string Name { get; }
	public string UniqueAlias { get; protected set; }

	public string Category { get; }
	public string Description { get; }

	public CheckListType CheckListType => CheckListType.Automatic;
    public string Trigger => String.Empty;
}



