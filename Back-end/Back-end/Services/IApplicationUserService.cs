using Back_end.Models;
using Back_end.Utilities;




namespace Back_end.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUser> GetAll(int PageNumber, int PageSize);

        ApplicationUser GetUserById(int UserId);

        void AddUser(ApplicationUser user);

        void UpdateUser(ApplicationUser user);

        void DeleteUser(int UserId);

    }
}