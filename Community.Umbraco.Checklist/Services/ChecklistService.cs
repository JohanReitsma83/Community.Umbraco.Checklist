using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Models;
using Community.Umbraco.Checklist.Repository;

namespace Community.Umbraco.Checklist.Services
{
	public interface IChecklistService
	{
		IList<CheckListEntry> GetAll();

        IList<CheckListEntry> GetAllOType(CheckListType type);

        IList<IGrouping<string, CheckListEntry>> GetAllGroupedByCategory();
        CheckListEntry GetbyId(int id);
		CheckListEntry? GetbyKey(string key);
		bool Save(CheckListEntry entry);
		void Delete(int id);
	}

	internal class ChecklistService : IChecklistService
	{
		private readonly ICheckListItemRepository _repository;

		public ChecklistService(ICheckListItemRepository repository)
		{
			_repository = repository;
		}

		public IList<CheckListEntry> GetAll()
		{
			return _repository.GetAll().ToList();
		}

        public IList<CheckListEntry> GetAllOType(CheckListType type)
        {
            return GetAll().Where(r => r.RunType == type).ToList();
        }

        public IList<IGrouping<string, CheckListEntry>> GetAllGroupedByCategory()
        {
            return _repository.GetAll().GroupBy(item => item.Category).ToList();
        }


        public CheckListEntry GetbyId(int id)
		{
			return _repository.GetbyId(id);
		}

		public CheckListEntry? GetbyKey(string key)
		{
			return _repository.GetbyKey(key);
		}


		public bool Save(CheckListEntry entry)
		{
			return _repository.Save(entry);
		}

		public void Delete(int id)
		{
			_repository.Delete(id);
		}
	}
}
