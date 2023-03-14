using InventoryManager.BLL.Implementation;
using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.models;
using InventoryManager.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(new UserViewModel());
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.UserRegister(user);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    //return RedirectToAction("Login");
                    return View("Login");
                }

                ViewBag.ErrMsg = msg;
                return View("Register");
            }

            return View("Register");
        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var (sucessful, msg, user) = await _userService.UserLogin(userViewModel);
                if (sucessful)
                {
                    ViewBag.UserLoggedID = user.Id;
                    ViewBag.UserLoggedName = user.FullName;
                    return View("Index");
                    // return RedirectToAction("Register");
                }

                ViewBag.ErrMsg = msg;
                return View("Login");

            }
            return View("Login");

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!(id > 0))
            {
                return NotFound();
            }
            var userFound = await _userService.GetUserDetail(id);
            if (userFound == null)
            {
                return NotFound();
            }

            return View(userFound);
        }

        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
        var (success,msg) =  await _userService.UpdateUserProfile(model);

            if (success)
            {
                @ViewBag.SuccMsg = msg;
                return View("Edit", model);
            }
            @ViewBag.ErrMsg = msg;
            return View("Edit",model);
        }

    }
}
