using Community.Umbraco.Checklist.Core.Enums;

namespace Community.Umbraco.Checklist.Core.Interfaces;

public interface ICheckListItem
{
	string Name { get; }

	public string UniqueAlias { get; }

	string Category { get; }


	public string Description { get; }

	public CheckListType CheckListType { get;  }

	public string Trigger { get;  }

}