using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace API.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteItem(ApplicationUser user)
        {
            ApplicationUser U = context.Users.Find(user);
            context.Users.Remove(U);
            context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            ApplicationUser user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<ApplicationUser> GetAllItems()
        {
            return context.Users.ToList();
        }

        public ApplicationUser GetItemByID(int id)
        {
            ApplicationUser user = context.Users.Find(id);
            return user;
        }

        public void InsertItem(ApplicationUser item)
        {
            context.Users.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItem(ApplicationUser item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}