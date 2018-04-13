using System;
using System.Collections.Generic;

namespace API.Models.DAL
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();

        T GetItemByID(string id);

        void InsertItem(T item);

        void UpdateItem(T id);

        void DeleteItem(string id);

        void Save();
    }
}