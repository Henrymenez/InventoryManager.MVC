using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.models;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using System.Globalization;
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
                User userCreated = await _userRepo.AddAsync(newUser);
                // _unitOfWork.SaveChangesAsync();
                return userCreated != null ? (true, $"User: {user.FullName} was successfully created! Login") : (false, "Operation failed!");
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

        public async Task<ProfileViewModel> GetUserDetail(int id)
        {
          var user =  await _userRepo.GetByIdAsync(id);
            return new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                UserId = user.Id,
                Password = user.Password
            };
            
        }

        public async Task<(bool success, string msg)> UpdateUserProfile(ProfileViewModel model)
        {
            var user = await _userRepo.GetByIdAsync(model.UserId);
            if (user == null)
            {
                return (false, $"User with id: {model.UserId} wasn't found");
            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Password = model.Password;

            return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"User with Id:{model.UserId} was Updated") : (false, $"Update Failed");


        }
    }
}
