namespace Community.Umbraco.Checklist.Models;

public class ChecklistItemState
{
	public ChecklistItemState(bool hasChanged, string? state, string[] message)
	{
		HasChanged = hasChanged;
		State = state;
		Messages = message;
	}

	public bool HasChanged { get;  }

	public string? State {
		get;
		    
	}

	public string[] Messages { get; }

}