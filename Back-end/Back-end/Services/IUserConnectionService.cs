using Back_end.Models;

namespace Back_end.Services
{
    public interface IUserConnectionService
    {
        public void Add(UserConnection connection);

        public void Delete(UserConnection connection);

        public IEnumerable<UserConnection> GetAll(string UserId);
    }
}
