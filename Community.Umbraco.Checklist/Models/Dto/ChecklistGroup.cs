namespace Community.Umbraco.Checklist.Models.Dto;

public class ChecklistGroup
{
	public ChecklistGroup(string category, List<CheckListItem> entries)
	{
		Category = category;
		Entries = entries;
	}

	public string Category { get; set; }

    public List<CheckListItem> Entries { get; set; }
}