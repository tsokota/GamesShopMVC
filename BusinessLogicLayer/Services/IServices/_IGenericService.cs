using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLogicLayer.Services.IServices
{
    public interface IGenericService<T> where T : class
    {
        void New(T item);

        void Update(T item);

        bool Delete(T item);

        T GetById(int id);

        IEnumerable<T> GetAllItems();

        List<SelectListItem> GetAllItemsAsSelectListItems();

        bool CheckOnItem(T item);
    }
}
