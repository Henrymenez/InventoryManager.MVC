using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.models;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using TodoList.DAL.Repository;


namespace InventoryManager.BLL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
        }


        public async Task<(bool successful, string msg)> UserRegister(UserViewModel user)
        {
            try
            {
                if (user.Password != user.ConfirmPassword) return (false, "Password and Confirm password not the same");

                var newUser = new User
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Phone = user.Phone

                };
                _userRepo.Add(newUser);
                var rowChanges = await _unitOfWork.SaveChangesAsync();
                return rowChanges > 0 ? (true, $"User: {user.FullName} was successfully created!") : (false, "Operation failed!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool successful, string msg, User userModel)> UserLogin(LoginViewModel user)
        {
            try
            {
                User userExist = await _userRepo.GetSingleByAsync(u => u.Email == user.Email && u.Password == user.Password, tracking: true);
                if (userExist == null) return (false, "Incorrect email or password", null);

                var UserObj = new User
                {
                    Id = userExist.Id,
                    FullName = userExist.FullName,
                    Email = userExist.Email,
                    Phone = userExist.Phone
                };
                return (true, "Successfully Logged In", UserObj);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }


        }

    }
}
