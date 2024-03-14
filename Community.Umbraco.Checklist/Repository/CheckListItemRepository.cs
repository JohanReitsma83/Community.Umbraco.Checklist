using Community.Umbraco.Checklist.Models;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Extensions;

namespace Community.Umbraco.Checklist.Repository;
internal class CheckListItemRepository : ICheckListItemRepository
{
    private readonly IScopeProvider _scopeProvider;
    
    public CheckListItemRepository(
        IScopeProvider scopeProvider)
    {
        _scopeProvider = scopeProvider;
    }
    
    public IEnumerable<CheckListEntry> GetAll()
    {
        using var scope = _scopeProvider.CreateScope();

        var sql = scope.SqlContext.Sql().Select("*").From<CheckListEntry>();

        var results = scope.Database.Fetch<CheckListEntry>(sql).OrderBy(x => x.Category).ThenBy(y => y.Name);
        return results;
    }


    public CheckListEntry GetbyId(int id)
    {
        using var scope = _scopeProvider.CreateScope(autoComplete: true);

        var sql = scope.SqlContext.Sql().Select("*").From(Constants.DatabaseSchema.Tables.CheckListEntries).Where<CheckListEntry>(x => x.Id == id);

        return scope.Database.SingleOrDefault<CheckListEntry>(sql);
    }

    public CheckListEntry? GetbyKey(string key)
    {
	    using var scope = _scopeProvider.CreateScope(autoComplete: true);
	    var sql = scope.SqlContext.Sql().Select("*").From(Constants.DatabaseSchema.Tables.CheckListEntries).Where<CheckListEntry>(x => x.UniqueAlias == key);
	    return scope.Database.SingleOrDefault<CheckListEntry>(sql);
    }


    public bool Save(CheckListEntry entry)
    {
        if (entry == null)
        {
            throw new ArgumentNullException(nameof(entry));
        }

        using var scope = _scopeProvider.CreateScope();
        if (entry.Id == default)
        {
	        scope.Database.Insert(entry);
        }
        else
        {
	        scope.Database.Update(entry,entry.Id);
        }

        scope.Complete();

        return true;
    }

    public void Delete(int id)
    {
        var entry = GetbyId(id);

        using var scope = _scopeProvider.CreateScope(autoComplete: true);
        scope.Database.Delete(entry);

    }
}
