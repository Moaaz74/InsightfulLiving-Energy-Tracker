using Back_end.Models;
using Back_end.Repositories.Interfaces;
using Back_end.Utilities;



namespace Back_end.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddUser(ApplicationUser user)
        {
            _unitOfWork.Repository<ApplicationUser>().Add(user);
            _unitOfWork.Save();
        }

        public void DeleteUser(int UsertId)
        {
            ApplicationUser user = _unitOfWork.Repository<ApplicationUser>().GetById(UsertId);
            _unitOfWork.Repository<ApplicationUser>().Delete(user);
            _unitOfWork.Save();
        }

        public PagedResult<ApplicationUser> GetAll(int PageNumber, int PageSize)
        {
            int totalCount = 0;
            List<ApplicationUser> Users = new List<ApplicationUser>();
            try
            {
                int ExcludedRecords = (PageSize * PageNumber) - PageSize;

                Users = _unitOfWork.Repository<ApplicationUser>().GetAll()
                    .Skip(ExcludedRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.Repository<ApplicationUser>().GetAll().ToList().Count;


            }
            catch (Exception ex) { throw; }

            PagedResult<ApplicationUser> result = new PagedResult<ApplicationUser>()
            {
                Data = Users,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            return result;
        }

        public ApplicationUser GetUserById(int UserId)
        {
            return _unitOfWork.Repository<ApplicationUser>().GetById(UserId);
        }

        public void UpdateUser(ApplicationUser User)
        {
            ApplicationUser user = _unitOfWork.Repository<ApplicationUser>().GetById(User.Id);
            user.Name = User.Name;
            user.Email = User.Email;
            user.Age = User.Age;
            user.Password = User.Password;
            user.HomeId = User.HomeId;
            _unitOfWork.Repository<ApplicationUser>().Update(user);
            _unitOfWork.Save();
        }
    }
}