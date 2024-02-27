using Back_end.Models;
using Back_end.Repositories.Interfaces;
using Back_end.Utilities;




namespace Back_end.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public void AddUser(ApplicationUser user)
        {
            _unitOfWork.Repository<ApplicationUser>().Add(user);
            _unitOfWork.Save();
        }

        public void DeleteUser(int UserId)
        {
            ApplicationUser user = _unitOfWork.Repository<ApplicationUser>().GetById(UserId);
            _unitOfWork.Repository<ApplicationUser>().Delete(user);
            _unitOfWork.Save();
        }


        public ApplicationUser GetUserById(string UserId)
        {
            return _unitOfWork.Repository<ApplicationUser>().GetById(UserId);
        }

        public void UpdateUser(ApplicationUser User)
        {
            ApplicationUser user = _unitOfWork.Repository<ApplicationUser>().GetById(User.Id);
            user.UserName = User.UserName;
            user.Email = User.Email;
            user.PasswordHash = User.PasswordHash;
            _unitOfWork.Repository<ApplicationUser>().Update(user);
            _unitOfWork.Save(); 
        }
    }
} 