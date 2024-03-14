namespace Community.Umbraco.Checklist.Core.Models;

public abstract class ChecklistItemTrigger
{
    public abstract StateInformation GetStatus(StateInformation previousState);
}