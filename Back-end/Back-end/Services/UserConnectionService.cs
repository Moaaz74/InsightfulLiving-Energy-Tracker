using Back_end.Models;
using Back_end.Repositories.Interfaces;

namespace Back_end.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private IUnitOfWork _unitOfWork;

        public UserConnectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(UserConnection connection)
        {
            _unitOfWork.Repository<UserConnection>().Add(connection);
            _unitOfWork.Save();
        }

        public void Delete(UserConnection connection)
        {
            _unitOfWork.Repository<UserConnection>().Delete(connection);
            _unitOfWork.Save();
        }

        public IEnumerable<UserConnection> GetAll(string UserId)
        {
            return _unitOfWork.Repository<UserConnection>().GetAll(filter: x => x.UserId == UserId);
        }
    }
}
