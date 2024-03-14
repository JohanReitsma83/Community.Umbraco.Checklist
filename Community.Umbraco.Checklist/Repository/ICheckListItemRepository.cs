
using Community.Umbraco.Checklist.Models;

namespace Community.Umbraco.Checklist.Repository;
public interface ICheckListItemRepository
{
	IEnumerable<CheckListEntry> GetAll();

    CheckListEntry GetbyId(int id);

    CheckListEntry? GetbyKey(string key);

    bool Save(CheckListEntry entry);

    void Delete(int id);
}
