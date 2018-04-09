namespace API.Models.DAL
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void DeleteItem(ApplicationUser user);
    }
}