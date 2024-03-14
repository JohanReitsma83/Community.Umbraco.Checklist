using NPoco;
using Community.Umbraco.Checklist.Core.Enums;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Community.Umbraco.Checklist.Models;

[TableName(Constants.DatabaseSchema.Tables.CheckListEntries)]
[PrimaryKey("Id", AutoIncrement = true)]
[ExplicitColumns]
public class CheckListEntry
{
	[PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
    [Column("Id")]
    public int Id { get; set; }

    [Column("UniqueAlias")]
    public string UniqueAlias { get; set; } = string.Empty;


    [Column("Category")]
    public string Category { get; set; } = string.Empty;

    [Column("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("Description")]
    public string Description { get; set; } = string.Empty;

    [Column("LastExecutedBy")]
    public string? LastExecutedBy { get; set; }

    [Column("LastRun")]
    [NullSetting(NullSetting = NullSettings.Null)]
    public DateTime? LastRun { get; set; }

    [Column("Trigger")]
    [NullSetting(NullSetting = NullSettings.Null)]
    public string Trigger { get; set; } = string.Empty;


    [Column("LastStatus")]
    public int _status { get; set; }

    [Ignore]
    public CheckListItemStatusType LastStatus
    {
	    get => (CheckListItemStatusType)_status;
	    set => _status = (int)value;
    }

    [Column("Messages")]
    public string? Messages { get; set; } = string.Empty;

    [Column("RunType")]
    public int _runType { get; set; }

    [Ignore]
    public CheckListType RunType {
	    get => (CheckListType)(_runType);
	    set => _runType = (int)value;
    }
    


    [Column("State")]
    [NullSetting(NullSetting = NullSettings.Null)]
    public string? State { get; set; } = string.Empty;
}
