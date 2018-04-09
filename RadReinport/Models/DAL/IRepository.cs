using System;
using System.Collections.Generic;

namespace API.Models.DAL
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();

        T GetItemByID(int id);

        void InsertItem(T item);

        void UpdateItem(T item);

        void DeleteItem(int id);

        void Save();
    }
}