using Back_end.Models;
using Back_end.Utilities;


namespace Back_end.Services
{
    public interface IUserService
    {
        PagedResult<ApplicationUser> GetAll(int PageNumber, int PageSize);
        ApplicationUser GetUserById(string UserId);

        void AddUser(ApplicationUser user);

        void UpdateUser(ApplicationUser user);

        void DeleteUser(int UserId);

    } 
}