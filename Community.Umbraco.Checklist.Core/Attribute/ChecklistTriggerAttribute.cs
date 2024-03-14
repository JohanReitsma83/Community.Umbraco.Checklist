namespace Community.Umbraco.Checklist.Core.Attribute;

[AttributeUsage(AttributeTargets.Class)]

public sealed class ChecklistTriggerAttribute : System.Attribute
{
    public string TriggerName { get; }

    public ChecklistTriggerAttribute(string name)
    {
        TriggerName = name;
    }

}