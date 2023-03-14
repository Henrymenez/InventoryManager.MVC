using InventoryManager.BLL.models;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.BLL.Interfaces
{
    public interface IUserService
    {
        Task<(bool successful, string msg)> UserRegister(UserViewModel user);

        Task<(bool successful, string msg,User userModel)> UserLogin(LoginViewModel user);

        Task<ProfileViewModel> GetUserDetail(int id);
    }
}
